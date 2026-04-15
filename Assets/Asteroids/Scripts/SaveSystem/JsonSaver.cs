using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Asteroids.Scripts.SaveSystem
{
    public class JsonSaver
    {
        private readonly string _filename = "SaveData.json";

        public void Save(SaveData saveData)
        {
            // Строка будет содержать сериализованный JSON объект
            string json = JsonConvert.SerializeObject(saveData);

            // Строка содержит путь, по которому будет храниться файл
            string path = Path.Combine(Application.persistentDataPath, _filename);

            // Создаем новый файл и через метод Write записываем сериализованный JSON объект
            using (StreamWriter writer = new(path))
            {
                writer.Write(json);
            }
        }

        public bool Load(ref SaveData saveData)
        {
            string loadFilename = GetSaveFilename();

            // only run if we find the filename on disk
            if (File.Exists(loadFilename))
            {
                string path = Path.Combine(Application.persistentDataPath, _filename);

                using (StreamReader reader = new(path))
                {
                    string json = reader.ReadToEnd();

                    saveData = JsonConvert.DeserializeObject<SaveData>(json);
                }

                return true;
            }

            return false;
        }

        private string GetSaveFilename()
        {
            return Application.persistentDataPath + "/" + _filename;
        }
    }
}