using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantManagement.Models;
using RestaurantManagement.Persistence;

namespace RestaurantManagement.BusinessLogic
{
    public class ReceiptManager
    {
        private List<Receipt> _receipts;
        private readonly MenuManager _menuManager;
        private readonly WaiterManager _waiterManager;
        private const string FileName = "receipts.json";

        public ReceiptManager(MenuManager menuManager, WaiterManager waiterManager)
        {
            _menuManager = menuManager;
            _waiterManager = waiterManager;
            _receipts = FileRepository.Load(FileName, new List<Receipt>());
        }

        public Receipt OpenReceipt(int tableNumber, Guid waiterId)
        {
            if (tableNumber < 1 || tableNumber > 50)
                throw new ArgumentException("Номер столика має бути від 1 до 50");

            var waiter = _waiterManager.GetById(waiterId)
                ?? throw new ArgumentException("Офіціанта не знайдено");

            var receipt = new Receipt
            {
                TableNumber = tableNumber,
                WaiterId = waiterId,
                Waiter = waiter
            };

            _receipts.Add(receipt);
            SaveToFile();
            return receipt;
        }

        public void AddOrder(Receipt receipt, Guid menuItemId, int quantity) { /* той самий код, що був */ }

        public void CloseReceipt(Receipt receipt)
        {
            if (receipt.Status != ReceiptStatus.Open)
                throw new InvalidOperationException("Чек вже закрито!");

            receipt.Status = ReceiptStatus.Closed;
            receipt.CloseTime = DateTime.Now;
            SaveToFile();
        }

        // Статистика по чайовым
        public Dictionary<string, decimal> TipsByWaiter() => _receipts
            .Where(r => r.Status == ReceiptStatus.Closed)
            .GroupBy(r => r.Waiter?.FullName ?? "Невідомий")
            .ToDictionary(g => g.Key, g => g.Sum(r => r.TipsAmount));

        public decimal TotalRevenue() => _receipts.Where(r => r.Status == ReceiptStatus.Closed).Sum(r => r.TotalAmount);
        public decimal TodayRevenue() => _receipts.Where(r => r.Status == ReceiptStatus.Closed && r.CloseTime?.Date == DateTime.Today).Sum(r => r.TotalAmount);
        public Dictionary<int, decimal> RevenueByTable() => _receipts.Where(r => r.Status == ReceiptStatus.Closed).GroupBy(r => r.TableNumber).ToDictionary(g => g.Key, g => g.Sum(r => r.TotalAmount));

        public IReadOnlyList<Receipt> GetAll() => _receipts;
        public Receipt? GetById(Guid id) => _receipts.FirstOrDefault(x => x.Id == id);

        private void SaveToFile() => FileRepository.Save(FileName, _receipts);
    }
}