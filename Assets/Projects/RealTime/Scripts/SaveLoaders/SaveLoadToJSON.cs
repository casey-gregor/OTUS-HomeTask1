using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace RealTime
{
    public class SaveLoadToJson
    {
        private const string JSON_FILE = "Chests.json";
        private readonly string filePath = Path.Combine(Application.dataPath, "Projects/RealTime/JSON", JSON_FILE);
        
        public ChestCollection LoadChests()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                ChestCollection chestCollection = JsonConvert.DeserializeObject<ChestCollection>(json);
                return chestCollection;
            }
            Debug.LogWarning("File not found: " + filePath);
            return null;
        }
        
        public void SaveChests(ChestCollection chestCollection)
        {
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string json = JsonConvert.SerializeObject(chestCollection);
            File.WriteAllText(filePath, json);
        }
    }
}