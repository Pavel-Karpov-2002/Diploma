using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializeContent
{
    public static void SerializeGameData(string path)
    {
        var settings = new JsonSerializerSettings();
        settings.TypeNameHandling = TypeNameHandling.Auto;
        string json = JsonConvert.SerializeObject(GameData.Data, settings);
        Serialize(path, json);
    }

    public static void Serialize(string path, object data)
    {
        using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, data);
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
        }
    }

    public static T Desiralize<T>(string path)
    {
        using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
        {
            try
            {
                if (stream != null)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    T data = (T)formatter.Deserialize(stream);
                    return data;
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
            return default(T);
        }
    }

    public static T GetFromJsonFile<T>(string path)
    {
        try
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                try
                {
                    return JsonConvert.DeserializeObject<T>(json);
                }
                catch
                {
                    return default(T);
                }
            }
        }
        catch
        {
            return default(T);
        }
    }
}