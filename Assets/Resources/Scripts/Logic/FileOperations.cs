using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

public class FileOperations
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

    public static bool WriteTextFile(string path, string content)
    {
        try
        {
            StreamWriter writer = new StreamWriter(path);
            writer.WriteLine(content);
            writer.Close();

            return true;
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(e.Message);
            return false;
        }
    }

    public static bool WriteByteFile(string path, byte[] bytes)
    {
        try
        {
            StreamWriter writer = new StreamWriter(path);
            writer.BaseStream.Write(bytes);
            writer.Close();

            return true;
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(e.Message);
            return false;
        }
    }
}
