using System;
using System.Linq;
using RestaurantManagement.BusinessLogic;
using RestaurantManagement.Models;

namespace RestaurantManagement.Presentation
{
    public class ConsoleMenu
    {
        private readonly MenuManager _menuManager;
        private readonly ReceiptManager _receiptManager;
        private readonly WaiterManager _waiterManager;

        public ConsoleMenu(MenuManager menuManager, ReceiptManager receiptManager, WaiterManager waiterManager)
        {
            _menuManager = menuManager;
            _receiptManager = receiptManager;
            _waiterManager = waiterManager;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==========================================");
                Console.WriteLine("       РЕСТОРАН ");
                Console.WriteLine("==========================================");
                Console.WriteLine("1. Меню страв");
                Console.WriteLine("2. Офіціанти");
                Console.WriteLine("3. Відкрити новий чек");
                Console.WriteLine("4. Додати замовлення до чеку");
                Console.WriteLine("5. Закрити чек");
                Console.WriteLine("6. Усі чеки");
                Console.WriteLine("7. Статистика та чайові");
                Console.WriteLine("0. Вихід");
                Console.Write("Оберіть пункт: ");

                switch (Console.ReadLine()?.Trim())
                {
                    case "1": ManageMenu(); break;
                    case "2": ManageWaiters(); break;
                    case "3": OpenReceipt(); break;
                    case "4": AddOrder(); break;
                    case "5": CloseReceipt(); break;
                    case "6": ShowAllReceipts(); break;
                    case "7": ShowStatistics(); break;
                    case "0": return;
                    default: Console.WriteLine("Невірний вибір!"); Console.ReadKey(); break;
                }
            }
        }

        private void ManageWaiters()
        {
            Console.Clear();
            Console.WriteLine("=== СПИСОК ОФІЦІАНТІВ ===");
            var waiters = _waiterManager.GetAll().ToList();
            for (int i = 0; i < waiters.Count; i++)
                Console.WriteLine($"{i + 1}. {waiters[i].FullName} (ID: {waiters[i].Id})");
            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey();
        }

        private void OpenReceipt()
        {
            Console.Clear();
            Console.Write("Номер столика (1-50): ");
            if (!int.TryParse(Console.ReadLine(), out int table) || table < 1 || table > 50)
            {
                Console.WriteLine("Невірний номер столика!");
                Console.ReadKey(); return;
            }

            Console.WriteLine("\nОберіть офіціанта:");
            var waiters = _waiterManager.GetAll().ToList();
            for (int i = 0; i < waiters.Count; i++)
                Console.WriteLine($"{i + 1}. {waiters[i].FullName}");

            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > waiters.Count)
            {
                Console.WriteLine("Невірний вибір офіціанта!");
                Console.ReadKey(); return;
            }

            var selectedWaiter = waiters[choice - 1];
            var receipt = _receiptManager.OpenReceipt(table, selectedWaiter.Id);

            Console.WriteLine($"\nЧек успішно відкрито!");
            Console.WriteLine($"Столик №{table} | Офіціант: {selectedWaiter.FullName} | ID чеку: {receipt.Id}");
            Console.ReadKey();
        }

        private void ManageMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== МЕНЮ СТРАВ ===");
                ShowMenu();
                Console.WriteLine("\n1. Додати страву");
                Console.WriteLine("2. Редагувати страву");
                Console.WriteLine("3. Видалити страву");
                Console.WriteLine("0. Назад");
                Console.Write("Вибір: ");

                switch (Console.ReadLine()?.Trim())
                {
                    case "1":
                        Console.Write("Назва страви: "); var name = Console.ReadLine() ?? "";
                        Console.Write("Ціна (грн): "); decimal.TryParse(Console.ReadLine(), out var price);
                        try
                        {
                            _menuManager.Add(new MenuItem { Name = name, Price = price });
                            Console.WriteLine("Страву додано!");
                        }
                        catch (Exception ex) { Console.WriteLine("Помилка: " + ex.Message); }
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Write("ID страви для редагування: ");
                        if (Guid.TryParse(Console.ReadLine(), out Guid editId))
                        {
                            var item = _menuManager.GetById(editId);
                            if (item != null)
                            {
                                Console.Write($"Нова назва ({item.Name}): "); var newName = Console.ReadLine();
                                Console.Write($"Нова ціна ({item.Price}): "); decimal.TryParse(Console.ReadLine(), out var newPrice);
                                item.Name = string.IsNullOrWhiteSpace(newName) ? item.Name : newName;
                                item.Price = newPrice != 0 ? newPrice : item.Price;
                                _menuManager.Update(item);
                                Console.WriteLine("Оновлено!");
                            }
                        }
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.Write("ID страви для видалення: ");
                        if (Guid.TryParse(Console.ReadLine(), out Guid delId))
                        {
                            try { _menuManager.Delete(delId); Console.WriteLine("Видалено!"); }
                            catch { Console.WriteLine("Не знайдено"); }
                        }
                        Console.ReadKey();
                        break;

                    case "0": return;
                }
            }
        }

        private void AddOrder()
        {
            ShowAllReceipts();
            Console.Write("ID чеку: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid receiptId)) { Console.WriteLine("Помилка"); Console.ReadKey(); return; }

            var receipt = _receiptManager.GetById(receiptId);
            if (receipt == null || receipt.Status != ReceiptStatus.Open)
            {
                Console.WriteLine("Чек не знайдено або вже закрито!");
                Console.ReadKey(); return;
            }

            ShowMenu();
            Console.Write("ID страви: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid itemId)) { Console.WriteLine("Помилка"); Console.ReadKey(); return; }

            Console.Write("Кількість: ");
            if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0) { Console.WriteLine("Помилка"); Console.ReadKey(); return; }

            try
            {
                _receiptManager.AddOrder(receipt, itemId, qty);
                Console.WriteLine("Замовлення додано!");
            }
            catch (Exception ex) { Console.WriteLine("Помилка: " + ex.Message); }
            Console.ReadKey();
        }

        private void CloseReceipt()
        {
            ShowAllReceipts();
            Console.Write("ID чеку для закриття: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid id)) { Console.WriteLine("Помилка"); Console.ReadKey(); return; }

            var receipt = _receiptManager.GetById(id);
            if (receipt == null || receipt.Status != ReceiptStatus.Open)
            {
                Console.WriteLine("Чек не знайдено або вже закрито");
                Console.ReadKey(); return;
            }

            _receiptManager.CloseReceipt(receipt);
            Console.WriteLine($"ЧЕК ЗАКРИТО! Сума: {receipt.TotalAmount:F2} грн");
            Console.WriteLine($"Чайові офіціанту ({receipt.Waiter?.FullName}): {receipt.TipsAmount:F2} грн");
            Console.ReadKey();
        }

        private void ShowAllReceipts()
        {
            Console.Clear();
            Console.WriteLine("=== УСІ ЧЕКИ ===");
            foreach (var r in _receiptManager.GetAll())
            {
                Console.WriteLine($"Чек {r.Id} | Столик №{r.TableNumber} | Офіціант: {r.Waiter?.FullName ?? "—"} | {r.Status}");
                Console.WriteLine($"   Відкрито: {r.OpenTime:HH:mm dd.MM.yyyy}");
                if (r.Status == ReceiptStatus.Closed)
                    Console.WriteLine($"   Закрито: {r.CloseTime:HH:mm} | СУМА: {r.TotalAmount:F2} грн | Чайові: {r.TipsAmount:F2} грн");

                foreach (var o in r.Orders)
                    Console.WriteLine($"   • {o.MenuItem?.Name} × {o.Quantity} = {o.Quantity * o.MenuItem?.Price:F2} грн");
                Console.WriteLine();
            }
            Console.WriteLine("Натисніть будь-яку клавішу...");
            Console.ReadKey();
        }

        private void ShowMenu()
        {
            Console.WriteLine("=== МЕНЮ ===");
            foreach (var m in _menuManager.GetAll())
                Console.WriteLine($"{m.Id} | {m.Name} — {m.Price:F2} грн");
        }

        private void ShowStatistics()
        {
            Console.Clear();
            Console.WriteLine("================ СТАТИСТИКА ================");
            Console.WriteLine($"Загальна виручка: {_receiptManager.TotalRevenue():F2} грн");
            Console.WriteLine($"Виручка за сьогодні ({DateTime.Today:dd.MM.yyyy}): {_receiptManager.TodayRevenue():F2} грн");
            Console.WriteLine("\nЧайові офіціантам (10% від закритих чеків):");
            var tips = _receiptManager.TipsByWaiter();
            if (tips.Count == 0) Console.WriteLine("   Ще немає закритих чеків");
            else
                foreach (var t in tips)
                    Console.WriteLine($"   • {t.Key}: {t.Value:F2} грн");

            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey();
        }
    }
}