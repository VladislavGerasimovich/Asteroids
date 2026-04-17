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
            string json = JsonConvert.SerializeObject(saveData);
            string path = Path.Combine(Application.persistentDataPath, _filename);

            using (StreamWriter writer = new(path))
            {
                writer.Write(json);
            }
        }

        public bool Load(ref SaveData saveData)
        {
            string loadFilename = GetSaveFilename();

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