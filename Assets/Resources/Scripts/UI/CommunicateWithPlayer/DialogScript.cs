using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class DialogScript : MonoBehaviour
{
    [SerializeField] private DialogParameters dialogParameters;
    [SerializeField] protected TextMeshProUGUI questionText;

    public delegate NPCQuestions GetQuestionsDelegate(string path);
    public static event GetQuestionsDelegate GetQuestions;

    private HashSet<int> enteringQuestion;
    private NPCQuestionsInformation questionsInformation;
    private Question[] questions;
    private int numberQuestion;
    private int countQuestionPerOneNPC;
    private NPCQuestions npcQuestions;

    public static string Path { get; set; }
    public static NPCType NpcType { get; set; }
    protected DialogParameters DialogParameters => dialogParameters;

    private void Awake()
    {
        enteringQuestion = new HashSet<int>();
        questionText.font = DialogParameters.QuestionFontAsset;

        TimerDialogScript.TimerEnd += ShowNewQuestion;
        TimerDialogScript.TimerEnd += TakeAwayPoints;

        npcQuestions = GetQuestions?.Invoke(Path);
        countQuestionPerOneNPC = npcQuestions.Student.AmountQuestionsForTest;

        DialogPanelSingleton.GetInstance().NpcQuestions = npcQuestions;
        DialogPanelSingleton.ShowQuestionDialog += ShowNewQuestion;
    }

    public void ShowNewQuestion()
    {
        SetQuestionType();
        if (!ExitDialog())
            InitializeQuestion();
    }

    public abstract void WriteQuestion(Question question);

    private void InitializeQuestion()
    {
        questions = questionsInformation.Questions;
        numberQuestion = GetRandomNumber.GenerateRandomNumberNotUsed(0, questions.Length, enteringQuestion);

        if (numberQuestion == -1)
        {
            ExitDialog();
            return;
        }

        enteringQuestion.Add(numberQuestion);
        TimerDialogScript.GetInstance().StartTimer(questions[numberQuestion].QuestionTime);
        ChangeActiveTestingPanel(questions[numberQuestion].IsTest(), questions[numberQuestion]);
    }

    private void ChangeActiveTestingPanel(bool isActive, Question question)
    {
        DialogPanelSingleton.GetInstance().EnteringResponse.gameObject.SetActive(!isActive);
        DialogPanelSingleton.GetInstance().Testing.gameObject.SetActive(isActive);

        if (isActive)
            DialogPanelSingleton.GetInstance().Testing.WriteQuestion(question);
        else
            DialogPanelSingleton.GetInstance().EnteringResponse.WriteQuestion(question);
    }

    private void SetQuestionType()
    {
        if (questionsInformation == null)
        {
            if (NpcType == NPCType.Student)
            {
                questionsInformation = npcQuestions.Student;
                countQuestionPerOneNPC = npcQuestions.Student.AmountQuestionsForTest + 1;
            }
            else
            {
                countQuestionPerOneNPC = npcQuestions.Teacher.AmountQuestionsForTest + 1;
                questionsInformation = npcQuestions.Teacher;
            }
        }
    }

    private void TakeAwayPoints()
    {
        PlayerScores.GetInstance().ChangeScores(-questions[numberQuestion].PointsForWrongAnswer);
    }

    private bool ExitDialog()
    {
        countQuestionPerOneNPC--;
        if (countQuestionPerOneNPC <= 0 || numberQuestion == -1)
        {
            questionsInformation = null;
            CloseDialogWindow();
            return true;
        }
        return false;
    }

    public void CloseDialogWindow()
    {
        DialogPanelSingleton.GetInstance().gameObject.SetActive(false);
        MovementJoystick.GetInstance().gameObject.SetActive(true);
        AnswerButton.ResetOnPlayerAnswered();
        enteringQuestion.Clear();
        countQuestionPerOneNPC = 0;
    }

    private void OnDestroy()
    {
        GetQuestions = null;
    }
}
