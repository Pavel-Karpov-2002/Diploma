using Newtonsoft.Json;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public static class QuestionLogic 
{
    public static NPCQuestions GetFileQuestions(string filePath)
    {
        return GetFromFile(filePath);
    }

    public static NPCQuestions GetQuestionsFromSite(string url)
    {
        return (NPCQuestions)ProcessRequest(url);
    }

    private static IEnumerator ProcessRequest(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Error: " + request.error);
            }
            else
            {
                yield return JsonConvert.DeserializeObject<NPCQuestions>(request.downloadHandler.text);
            }
        }
    }

    private static NPCQuestions GetOnline(UnityWebRequest request)
    {
        if (request.result == UnityWebRequest.Result.ProtocolError)
            return null;

        try
        {
            return JsonConvert.DeserializeObject<NPCQuestions>(request.downloadHandler.text);
        }
        catch
        {
            return null;
        }
    }

    private static NPCQuestions GetFromFile(string path)
    {
        try
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                try
                {
                    return JsonConvert.DeserializeObject<NPCQuestions>(json);
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
