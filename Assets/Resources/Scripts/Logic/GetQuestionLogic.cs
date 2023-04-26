using Newtonsoft.Json;
using System.IO;
using UnityEngine.Networking;

public static class QuestionLogic
{
    public static Question[] GetQuestion(string filePath, string url)
    {
        Question[] questions;
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SendWebRequest();

        if ((questions = GetOnline(request)) == null)
            questions = GetFromFile(filePath);

        return questions;
    }

    private static Question[] GetOnline(UnityWebRequest request)
    {
        if (request.result == UnityWebRequest.Result.ProtocolError)
            return null;

        try
        {
            return JsonConvert.DeserializeObject<Question[]>(request.downloadHandler.text);
        }
        catch
        {
            return null;
        }
    }

    private static Question[] GetFromFile(string path)
    {
        try
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                try
                {
                    return JsonConvert.DeserializeObject<Question[]>(json);
                }
                catch
                {
                    return null;
                }
            }
        }
        catch
        {
            return null;
        }
    }
}
