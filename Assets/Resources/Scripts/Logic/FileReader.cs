using Newtonsoft.Json;
using System;
using System.IO;

public class FileReader
{
    public static T ReadJsonWithTypes<T>(string text)
    {
        try
        {
            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            return JsonConvert.DeserializeObject<T>(text, settings);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(e.Message);
        }
        return default(T);
    }

    public static string ReadTextFile(string path)
    {
        StreamReader reader = new StreamReader(path);
        string text = reader.ReadToEnd();
        reader.Close();

        return text;
    }
}
