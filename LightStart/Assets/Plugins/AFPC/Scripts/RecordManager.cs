using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Features
{
    public class SaveData
    {
        public int Level;
        public float Result;

        public SaveData(int level, float result)
        {
            Level = level;
            Result = result;
        }
    }
    
    public static class RecordManager
    {
        public static void SaveLevelStage(int level, float result)
        {
            var binaryFormatter = new BinaryFormatter();
            var filePath = Application.persistentDataPath + "/LastRecord.txt";
            var fileStream = new FileStream(filePath, FileMode.OpenOrCreate);

            var saveData = new SaveData(level, result);
            
            binaryFormatter.Serialize(fileStream, saveData);
            fileStream.Close();
        }

        public static SaveData LoadRecords()
        {
            var filePath = Application.persistentDataPath + "/LastRecord.txt";
            if (File.Exists(filePath))
            {
                var binaryFormatter = new BinaryFormatter();
                var fileStream = new FileStream(filePath, FileMode.Open);

                var saveData = binaryFormatter.Deserialize(fileStream) as SaveData;
                return saveData;
            }
            return null;
        }
    }
}