using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class DialogScript : MonoBehaviour
{
    [SerializeField] private GameParameters gameParameters;
    [SerializeField] protected TextMeshProUGUI questionText;

    private int countQuestionPerOneNPC;
    private Question[] questions;
    private HashSet<int> enteringQuestion;
    private static DialogScript instance;

    protected Question[] Questions => questions;

    protected DialogParameters DialogParameters => gameParameters.Dialog;

    public int AmountQuestionPerOneNPC { get; set; }
    public static string FilePath { get; set; }
    public static string UrlSiteQuestion { get; set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        enteringQuestion = new HashSet<int>();
        FilePath = DialogParameters.FilePathForStudents;
        UrlSiteQuestion = DialogParameters.UrlForStudents;
        AmountQuestionPerOneNPC = DialogParameters.AmountQuestionPerOneNPC;
        RefreshQuestinoList(FilePath, UrlSiteQuestion);

        TimerDialogScript.TimerEnd += ShowNewQuestion;
        TimerDialogScript.TimerEnd += TakeAwayPoints;
        NPC.OnUpdateQuestions += SetNewListQuestion;
    }

    public void ShowNewQuestion()
    {
        ExitDialog();
        RefreshQuestinoList(FilePath, UrlSiteQuestion);
        InitializeQuestion();
    }

    public void SetNewListQuestion(string filePath, string url)
    {
        questions = QuestionLogic.GetQuestion(filePath, url);
    }

    public abstract void WriteQuestion(int numQuestion);

    private void ExitDialog()
    { 
        countQuestionPerOneNPC++;

        if (countQuestionPerOneNPC > AmountQuestionPerOneNPC)
        {
            CloseDialogWindow();
            return;
        }
    }

    private void RefreshQuestinoList(string filePath, string url)
    {
        if (questions == null)
        {
            SetNewListQuestion(filePath, url);
            return;
        }

        if (questions.Length <= enteringQuestion.Count)
        {
            enteringQuestion.Clear();
            SetNewListQuestion(filePath, url);
        }
    }

    private void InitializeQuestion()
    {
        int numberQuestion = GetRandomNumber.GenerateRandomNumberNotUsed(0, questions.Length, enteringQuestion);
        enteringQuestion.Add(numberQuestion);

        switch (questions[numberQuestion].questionType)
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

        TimerDialogScript.GetInstance().StartTimer(questions[numberQuestion].questionTime);
    }

    public void CloseDialogWindow()
    {
        EnteringResponseScript.GetInstance().gameObject.SetActive(false);
        TestingAnswersScript.GetInstance().gameObject.SetActive(false);
        DialogPanelSingleton.GetInstance().gameObject.SetActive(false);
        MovementJoystick.GetInstance().gameObject.SetActive(true);
        AnswerButton.ResetOnPlayerAnswered();
        enteringQuestion.Clear();
        
        countQuestionPerOneNPC = 0;
    }

    private void TakeAwayPoints()
    {
        ((PlayerScores)PlayerConstructor.GetInstance()).ChangeScores(false);
    }

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
