using Newtonsoft.Json;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public static class QuestionLogic 
{
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
}
