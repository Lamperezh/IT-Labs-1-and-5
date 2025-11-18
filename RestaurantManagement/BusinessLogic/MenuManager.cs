using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantManagement.Models;
using RestaurantManagement.Persistence;

namespace RestaurantManagement.BusinessLogic
{
    public class MenuManager
    {
        private List<MenuItem> _items;
        private const string FileName = "menu.json";

        public MenuManager()
        {
            // При запуске программы — загружаем меню из файла
            _items = FileRepository.Load(FileName, new List<MenuItem>());
        }

        public IReadOnlyList<MenuItem> GetAll() => _items;

        public void Add(MenuItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException("Название не может быть пустым");

            if (_items.Any(x => x.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Такая страва уже есть в меню!");

            _items.Add(item);
            SaveToFile(); // ← сохраняем сразу
        }

        public void Update(MenuItem item)
        {
            var existing = _items.FirstOrDefault(x => x.Id == item.Id)
                ?? throw new ArgumentException("Страва не найдена");
            existing.Name = item.Name;
            existing.Price = item.Price;
            SaveToFile(); // ← сохраняем
        }

        public void Delete(Guid id)
        {
            var item = _items.FirstOrDefault(x => x.Id == id)
                ?? throw new ArgumentException("Страва не найдена");
            _items.Remove(item);
            SaveToFile(); // ← сохраняем
        }

        public MenuItem? GetById(Guid id) => _items.FirstOrDefault(x => x.Id == id);

        private void SaveToFile()
        {
            FileRepository.Save(FileName, _items);
        }
    }
}