using System.IO;
using Newtonsoft.Json;

namespace RestaurantManagement.Persistence
{
    public static class FileRepository
    {
        private const string DataFolder = "Data";

        // Создаём папку Data, если её нет
        static FileRepository()
        {
            if (!Directory.Exists(DataFolder))
                Directory.CreateDirectory(DataFolder);
        }

        // Сохранить любой объект в JSON-файл
        public static void Save<T>(string fileName, T data)
        {
            string fullPath = Path.Combine(DataFolder, fileName);
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(fullPath, json);
        }

        // Загрузить объект из JSON-файла (если файла нет — вернёт defaultValue)
        public static T Load<T>(string fileName, T defaultValue)
        {
            string fullPath = Path.Combine(DataFolder, fileName);
            if (!File.Exists(fullPath))
                return defaultValue;

            string json = File.ReadAllText(fullPath);
            return JsonConvert.DeserializeObject<T>(json) ?? defaultValue;
        }
    }
}