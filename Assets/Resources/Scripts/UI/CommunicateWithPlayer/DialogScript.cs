using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using static TimerDialogScript;

public abstract class DialogScript : MonoBehaviour
{
    [SerializeField] private GameParameters gameParameters;
    [SerializeField] protected TextMeshProUGUI questionText;

    private Question[] questions;
    private IEnumerator getQuestion;
    private HashSet<int> enteringQuestion;
    private static DialogScript instance;
    private int countQuestionPerOneNPC;

    protected Question[] Questions => questions;
    protected DialogParameters DialogParameters => gameParameters.Dialog;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        enteringQuestion = new HashSet<int>();
        getQuestion = GetQuestion(DialogParameters.Url, DialogParameters.FilePath);
        StartCoroutine(getQuestion);
        TimerDialogScript.TimerEnd += ShowNewQuestion;
    }

    public IEnumerator GetQuestion(string url, string path)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if ((questions = GetOnline(request)) == null)
            questions = GetFromFile(path);
    }

    private Question[] GetOnline(UnityWebRequest request)
    {
        if (request.result == UnityWebRequest.Result.ProtocolError)
        {
            return null;
        }

        try
        {
            return JsonConvert.DeserializeObject<Question[]>(request.downloadHandler.text);
        }
        catch
        {
            return null;
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

    public void ShowNewQuestion()
    {
        if (enteringQuestion == null)
            enteringQuestion = new HashSet<int>();

        countQuestionPerOneNPC++;

        if (countQuestionPerOneNPC >= DialogParameters.AmountQuestionPerOneNPC + 1)
        {
            CloseDialogWindow();
            return;
        }

        if (questions.Length <= enteringQuestion.Count)
            enteringQuestion.Clear();

        InitializeQuestion();
    }

    private void CloseDialogWindow()
    {
        EnteringResponseScript.GetInstance().gameObject.SetActive(false);
        TestingAnswersScript.GetInstance().gameObject.SetActive(false);
        DialogPanelSingleton.GetInstance().gameObject.SetActive(false);
        MovementJoystick.GetInstance().gameObject.SetActive(true);
        countQuestionPerOneNPC = 0;
    }

    private void InitializeQuestion()
    {
        int numberQuestion = GetRandomNumber.GenerateRandomNumberNotUsed(0, questions.Length, enteringQuestion);
        enteringQuestion.Add(numberQuestion);

        switch (Questions[numberQuestion].questionType)
        {
            case (Question.QuestionType.Test):
                EnteringResponseScript.GetInstance().gameObject.SetActive(false);
                TestingAnswersScript.GetInstance().gameObject.SetActive(true);
                TestingAnswersScript.GetInstance().WriteQuestion(numberQuestion);
                break;
            case (Question.QuestionType.Entering):
                TestingAnswersScript.GetInstance().gameObject.SetActive(false);
                EnteringResponseScript.GetInstance().gameObject.SetActive(true);
                EnteringResponseScript.GetInstance().WriteQuestion(numberQuestion);
                break;
        }

        TimerDialogScript.GetInstance().StartTimer(Questions[numberQuestion].questionTime);
    }

    public abstract void WriteQuestion(int numQuestion);

    public static DialogScript GetInstance()
    {
        if (instance == null)
            instance = FindObjectOfType<DialogScript>();

        return instance;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
