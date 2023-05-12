using Newtonsoft.Json;
using System.IO;

public class FileReader
{
    public static T ReadJsonWithTypes<T>(string text)
    {
        var settings = new JsonSerializerSettings();
        settings.TypeNameHandling = TypeNameHandling.Auto;
        return JsonConvert.DeserializeObject<T>(text, settings);
    }

    public static string ReadTextFile(string path)
    {
        StreamReader reader = new StreamReader(path);
        string text = reader.ReadToEnd();
        reader.Close();

        return text;
    }
}
