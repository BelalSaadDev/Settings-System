using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData<T>(T data, string fileName)
    {
        string path = Application.persistentDataPath + fileName;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static T LoadData<T>(string fileName) where T : class, new()
    {
        string path = Application.persistentDataPath + fileName;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new(path, FileMode.Open);

            T data = formatter.Deserialize(stream) as T;
            stream.Close();
            return data;
        }
        Debug.LogError("Save file not found in" + path + "\nCreating a new one...");
        return null;
    }
}
