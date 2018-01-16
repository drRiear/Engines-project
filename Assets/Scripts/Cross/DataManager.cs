using System.IO;
using System;
using UnityEngine;

public class DataManager 
{
    
    private readonly string path;

    public DataManager(string filename)
    {
        string relativeFilePath = "Saved Data/" + filename + ".json";
        path = Path.Combine(Application.dataPath, relativeFilePath);         //Application.persistentDataPath can be used instead Application.dataPath
    }
    
    public void CreateJSONFile()
    {
        if (File.Exists(path))
        {
            Debug.Log("Data file already exist. Rewriting this file.");
            return;
        }
        else
            File.Create(path);
    }

    public void SaveToJSON<T>(T obj)
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning("Data file does not exist. Creating it.");
            File.Create(path);
            return;
        }

        string jsonObj = JsonUtility.ToJson(obj);
        
        StreamWriter writer = new StreamWriter(path);
        writer.Write(jsonObj);
        writer.Close();
    }

    public T LoadFromJSON<T>()
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning("Data file does not exist. Nothing to load.");
            return default(T);
        }

        StreamReader reader = new StreamReader(path);
        string jsonString = reader.ReadToEnd();
        reader.Close();
        
        return JsonUtility.FromJson<T>(jsonString);
    }
}