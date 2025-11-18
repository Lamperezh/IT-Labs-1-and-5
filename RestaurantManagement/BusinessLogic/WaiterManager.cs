using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantManagement.Models;
using RestaurantManagement.Persistence;

namespace RestaurantManagement.BusinessLogic
{
    public class WaiterManager
    {
        private List<Waiter> _waiters;
        private const string FileName = "waiters.json";

        public WaiterManager()
        {
            _waiters = FileRepository.Load(FileName, new List<Waiter>());
            // Добавим пару официантов по умолчанию, если список пустой
            if (!_waiters.Any())
            {
                _waiters.AddRange(new[]
                {
                    new Waiter { FullName = "Іванов Іван Іванович" },
                    new Waiter { FullName = "Петренко Ольга Сергіївна" },
                    new Waiter { FullName = "Сидоренко Дмитро Олександрович" }
                });
                SaveToFile();
            }
        }

        public IReadOnlyList<Waiter> GetAll() => _waiters;

        public Waiter? GetById(Guid id) => _waiters.FirstOrDefault(w => w.Id == id);

        public void Add(Waiter waiter)
        {
            if (string.IsNullOrWhiteSpace(waiter.FullName))
                throw new ArgumentException("ПІБ не може бути порожнім");
            _waiters.Add(waiter);
            SaveToFile();
        }

        private void SaveToFile() => FileRepository.Save(FileName, _waiters);
    }
}