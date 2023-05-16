using Newtonsoft.Json;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GetFileScript : MonoBehaviour
{
    public void SaveJsonFile<T>(T data, string path)
    {
        string pathFolder = EditorUtility.OpenFolderPanel("Load json files", "", "");
        StreamWriter stream = new StreamWriter(pathFolder + "\\" + Path.GetFileName(path));
        var settings = new JsonSerializerSettings();
        settings.TypeNameHandling = TypeNameHandling.Auto;
        string jsonString = JsonConvert.SerializeObject(data, settings);
        stream.WriteLine(jsonString);
        stream.Close();
    }

    public void Decryption(bool isGameData)
    {
        string path = EditorUtility.OpenFilePanel("Overwrite with json", "", "json");
        if (path == string.Empty)
        {
            GameException.Instance.ShowError("Вы не указали путь к файлу");
            return;
        }
        if (isGameData)
            SaveJsonFile(FileReader.ReadJsonWithTypes<GameData>(FileEncryption.ReadFile(path)), path);
        else
            SaveJsonFile(FileReader.ReadJsonWithTypes<NPCQuestions>(FileEncryption.ReadFile(path)), path);
    }

    public void Encryption(bool isGameData)
    {
        string path = EditorUtility.OpenFilePanel("Overwrite with json", "", "json");
        var settings = new JsonSerializerSettings();
        settings.TypeNameHandling = TypeNameHandling.Auto;
        if (path == string.Empty)
        {
            GameException.Instance.ShowError("Вы не указали путь к файлу");
            return;
        }
        if (isGameData)
            FileEncryption.WriteFile(path, JsonConvert.DeserializeObject<GameData>(FileReader.ReadTextFile(path), settings));
        else
            FileEncryption.WriteFile(path, JsonConvert.DeserializeObject<NPCQuestions>(FileReader.ReadTextFile(path), settings));
    }
}
