using System.Collections.Generic;
using UnityEngine;

public class DialogScript : CustomSingleton<DialogScript>
{
    [SerializeField] private TestingAnswersScript testing;
    [SerializeField] private EnteringResponseScript enteringResponse;

    private HashSet<int> enteringQuestion;
    private NPCQuestionsInformation questionsInformation;
    private Question[] questions;
    private int numberQuestion;
    private int countQuestionPerOneNPC;

    public delegate void ShowQuestionDelegate();
    public static ShowQuestionDelegate ShowQuestionDialog;
    public delegate NPCQuestions GetQuestionsDelegate(string path);

    public NPCQuestions NpcQuestions { get; private set; }
    public NPCQuestionsInformation QuestionsInformation { get => questionsInformation; set => questionsInformation = value; }
    public EnteringResponseScript EnteringResponse => enteringResponse;
    public TestingAnswersScript Testing => testing;

    public static int countNPCCompleted;
    public static NPCType NpcType { get; set; }
    public static string Path { get; set; }
    public static event GetQuestionsDelegate GetQuestions;

    protected override void Awake()
    {
        base.Awake();
        enteringQuestion = new HashSet<int>();
        TimerDialogScript.TimerEnd += ShowNewQuestion;
        TimerDialogScript.TimerEnd += TakeAwayPoints;
        NpcQuestions = GetQuestions?.Invoke(Path);
    }

    private void Start()
    {
        gameObject.SetActive(false);
        enteringQuestion = new HashSet<int>();
    }

    public void ShowNewQuestion()
    {
        SetQuestionType();
        if (!ExitDialog())
            InitializeQuestion();
    }

    private void InitializeQuestion()
    {
        PlayerSkills.Instance.SetButtonsIntractable();
        numberQuestion = GetRandomNumber.GenerateRandomNumberNotUsed(0, questions.Length, enteringQuestion);
        if (numberQuestion == -1)
        {
            enteringQuestion.Clear();
            numberQuestion = GetRandomNumber.GenerateRandomNumberNotUsed(0, questions.Length, enteringQuestion);
        }
        enteringQuestion.Add(numberQuestion);
        TimerDialogScript.Instance.StartTimer(questions[numberQuestion].QuestionTime);
        ChangeActiveTestingPanel(questions[numberQuestion].IsTest(), questions[numberQuestion]);
    }

    private void ChangeActiveTestingPanel(bool isActive, Question question)
    {
        enteringResponse.gameObject.SetActive(!isActive);
        testing.gameObject.SetActive(isActive);

        if (isActive)
            testing.WriteQuestion(question);
        else
            enteringResponse.WriteQuestion(question);
    }

    private void SetQuestionType()
    {
        if (questionsInformation == null)
        {
            switch (NpcType)
            {
                case NPCType.Student:
                    questionsInformation = NpcQuestions.Student;
                    countQuestionPerOneNPC = NpcQuestions.Student.AmountQuestionsForTest + 1;
                    break;
                case NPCType.Teacher:
                    countQuestionPerOneNPC = NpcQuestions.Teacher.AmountQuestionsForTest + 1;
                    questionsInformation = NpcQuestions.Teacher;
                    PlayerSkills.Instance.SkillsPanel.SetActive(false);
                    break;
            }
            enteringQuestion.Clear();
            questions = questionsInformation.Questions;
        }
    }

    private void TakeAwayPoints()
    {
        PlayerScores.Instance.ChangeScores(-questions[numberQuestion].PointsForWrongAnswer);
    }

    private bool ExitDialog()
    {
        countQuestionPerOneNPC--;
        if (countQuestionPerOneNPC <= 0)
        {
            CloseDialogWindow();
            return true;
        }
        return false;
    }

    public void CloseDialogWindow()
    {
        DialogScript.Instance.gameObject.SetActive(false);
        MovementJoystick.Instance.gameObject.SetActive(true);
        AnswerButton.ResetOnPlayerAnswered();
        countQuestionPerOneNPC = 0;
        countNPCCompleted++;
    }

    private void OnDestroy()
    {
        ShowQuestionDialog = null;
    }
}
