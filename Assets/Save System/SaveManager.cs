

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Application = UnityEngine.Device.Application;

public class SaveManager
{
    public static void Save(FarmManager farmManager, StructureManager structureManager, InventoryManager inventoryManager, int dayCount)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "FarmingFrenzy.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(farmManager, structureManager, inventoryManager, dayCount);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData Load()
    {
        string path = Application.persistentDataPath + "FarmingFrenzy.sav";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in: " + path);
            return null;
        }
    }
}