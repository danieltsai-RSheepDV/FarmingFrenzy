﻿

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Application = UnityEngine.Device.Application;

public class SaveManager
{
    public static void Save(float version, FarmManager farmManager, StructureManager structureManager, InventoryManager inventoryManager, int dayCount)
    {
        SaveData data = new SaveData(farmManager, structureManager, inventoryManager, dayCount);
        
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/FarmingFrenzy" + version + ".sav";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData Load(float version)
    {
        string path = Application.persistentDataPath + "/FarmingFrenzy" + version + ".sav";
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