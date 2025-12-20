using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveData<T>
{
    private static string dataPath = Application.persistentDataPath + "/Player_Data/";//lo usaremos para crear una nueva carpeta
    
    public static void SerializeJSON(T itemToSave, string fileName)
    {
        //Unity no soporta listas en JSON por lo que pasamos un objeto de una clase que tenga de atributo una lista
        string jsonString = JsonUtility.ToJson(itemToSave, true);
        using (StreamWriter stream = File.CreateText(dataPath+fileName))
        {
            stream.WriteLine(jsonString);
        }
        Debug.Log("Data saved to: " + dataPath + fileName);
    }

    public static void DeserializeJSON(T itemToLoad, string fileName)
    {
        if (File.Exists(dataPath + fileName))
        {
            using (StreamReader stream = new StreamReader(dataPath + fileName))
            {
                var jsonString = stream.ReadToEnd();
                T itemLoaded = JsonUtility.FromJson<T>(jsonString);
                itemToLoad = itemLoaded;
            }
            Debug.Log("Data loaded from: " + dataPath + fileName);
        }
        else
        {
            Debug.LogWarning("File not found: " + dataPath + fileName);
        }
    }

    public static T DeserializeJSON(string fileName)
    {
        if (File.Exists(dataPath + fileName))
        {
            using (StreamReader stream = new StreamReader(dataPath + fileName))
            {
                var jsonString = stream.ReadToEnd();
                T itemLoaded = JsonUtility.FromJson<T>(jsonString);
                Debug.Log("Data loaded from: " + dataPath + fileName);
                return itemLoaded;
            }
        }
        else
        {
            Debug.LogWarning("File not found: " + dataPath + fileName);
        }
        return default;
    }

    public static bool SaveDataExists(string fileName)
    {
        return File.Exists(dataPath + fileName);
    }

    public static T InitializeData(string fileName)
    {
        if (SaveDataExists(fileName))
        {
            return DeserializeJSON(fileName);
        }
        else
        {
            Debug.Log("No existing data found. Creating new data file: " + dataPath + fileName);
            T newData = Activator.CreateInstance<T>();
            SerializeJSON(newData, fileName);
            return newData;
        }
    }
}
