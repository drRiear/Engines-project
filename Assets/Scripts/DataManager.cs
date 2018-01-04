using System.IO;
using System.Collections;
using UnityEngine;

public class DataManager 
{
    
    private string path;

    public DataManager(string filename)
    {
        string relativeFilePath = "Saved Data/" + filename + ".json";
        path = Path.Combine(Application.dataPath, relativeFilePath);         //Application.persistentDataPath can be used instead Application.dataPath
    }

    public void CreateJSONFile()
    {
        if (File.Exists(path))
        {
            Debug.LogWarning("Data file already exist.");
            return;
        }
        else
            File.Create(path);
    }

    public void SaveToJSON<T>(T obj)
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning("Data file does not exist. You need to create it first.");
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
            Debug.LogWarning("Data file does not exist. You need to create it first.");
            return default(T);
        }

        StreamReader reader = new StreamReader(path);
        string jsonString = reader.ReadToEnd();
        reader.Close();
        
        return JsonUtility.FromJson<T>(jsonString);
    }
}