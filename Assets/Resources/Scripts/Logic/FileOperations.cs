using Newtonsoft.Json;
using System;
using System.IO;

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
        using (StreamReader reader = new StreamReader(File.Open(path, FileMode.OpenOrCreate)))
        {
            string text = reader.ReadToEnd();
            reader.Close();

            return text;
        }
    }

    public static byte[] GetBytesInStream(Stream stream)
    {
        using (var memoryStream = new MemoryStream())
        {
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
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
