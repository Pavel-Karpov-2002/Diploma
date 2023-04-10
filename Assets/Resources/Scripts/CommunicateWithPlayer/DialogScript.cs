using Newtonsoft.Json;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public abstract class DialogScript : MonoBehaviour
{
    [SerializeField] protected GameParameters gameParameters;
    [SerializeField] protected TextMeshProUGUI questionText;


    private Question[] questions;
    private DialogParameters dialogParameters;
    private IEnumerator getQuestion;

    protected Question[] Questions => questions;
    protected DialogParameters DialogParameters => dialogParameters;

    private void Awake()
    {
        dialogParameters = gameParameters.Dialog;
        getQuestion = GetQuestion(dialogParameters.Url, dialogParameters.FilePath);
        StartCoroutine(getQuestion);
    }

    // "\\..\\The Labyrinth of GSTU\\Assets\\Resources\\Questions\\questions.json"
    public IEnumerator GetQuestion(string url, string path)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if ((questions = GetOnline(request)) == null)
        {
            questions = GetFromFile(path);
        }
    }

    private Question[] GetOnline(UnityWebRequest request)
    {
        if (request.result == UnityWebRequest.Result.ProtocolError)
        {
            return null;
        }
        else
        {
            try
            {
                return JsonConvert.DeserializeObject<Question[]>(request.downloadHandler.text);
            }
            catch
            {
                return null;
            }
        }
    }

    private Question[] GetFromFile(string path)
    {
        using (StreamReader reader = new StreamReader(path))
        {
            string json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<Question[]>(json);
        }
    }

    public abstract void WriteQuestion(int numQuestion);
}
