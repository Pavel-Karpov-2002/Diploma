using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConstructorTestScript : MonoBehaviour
{
    [SerializeField] private GameParameters gameParameters;
    [SerializeField] private GameObject panelAnswers;
    [SerializeField] private GameObject panelChangeAnswers;
    [SerializeField] private GameObject panelTeacher;
    [SerializeField] private GameObject panelStudent;
    [SerializeField] private ChangeQuestionButton buttonChangeQuestion;
    [SerializeField] private Button buttonUpdateQuestion;

    [SerializeField] private TMP_InputField inputEnteringQuestion;
    [SerializeField] private TMP_InputField inputAnswerButton;
    [SerializeField] private TMP_InputField correctAnswerInput;
    [SerializeField] private TMP_InputField pointsForCorrectAnswerInput;
    [SerializeField] private TMP_InputField pointsForWrongAnswerInput;
    [SerializeField] private TMP_InputField questionTimeButton;
    [SerializeField] private TMP_InputField amountStudentsOnFloorInput;
    [SerializeField] private TMP_InputField amountQuestionsForTestInput;
    [SerializeField] private TMP_InputField topicNameInput;

    [SerializeField] private TMP_InputField amountQuestionsForTestTeacherInput;
    [SerializeField] private TMP_InputField minPercentCorrectTeacherInput;
    [SerializeField] private TMP_InputField minPercentCorrectToUnlockTeacherInput;

    private List<TMP_InputField> answerInputButtons;
    private List<Question> questions;
    private NPCQuestions npcQuestions;

    private void Start()
    {
        answerInputButtons = new List<TMP_InputField>();
        questions = new List<Question>();
        panelStudent.SetActive(true);
        panelTeacher.SetActive(false);
    }

    public void AddAnswerButton(string textInput)
    {
        TMP_InputField input = Instantiate(inputAnswerButton);
        input.transform.SetParent(panelAnswers.transform);
        input.transform.localScale = inputAnswerButton.transform.localScale;
        input.transform.position = inputAnswerButton.transform.position;
        input.text = textInput;
        answerInputButtons.Add(input);
    }

    public void AddChangeAnswerButton()
    {
        if (CheckOnNullParameters())
            return;

        string[] answers = GetAnswers();
        if (answers == null)
            return;

        questions.Add(GetQuestion(answers));

        int count = questions.Count - 1;
        Question question = questions[count];
        ChangeQuestionButton button = CreateChangeButton(buttonChangeQuestion.Button, panelChangeAnswers).GetComponent<ChangeQuestionButton>();
        AddComponentsToButton(button, question, inputEnteringQuestion.text);
        ClearInputContent();
    }

    public void SetInformation(int npcType)
    {
        switch ((NPCType)npcType)
        {
            case NPCType.Student:
                if (IsStudentInformationFilled())
                {
                    npcQuestions = new NPCQuestions();
                    npcQuestions.Student = new NPCQuestionsInformation();
                    npcQuestions.Student.Questions = questions.ToArray();
                    npcQuestions.AmountStudentsOnFloor = int.Parse(amountStudentsOnFloorInput.text);
                    npcQuestions.Student.AmountQuestionsForTest = int.Parse(amountQuestionsForTestInput.text);
                    ClearAllContent(false);
                }
            break;
            case NPCType.Teacher:
                if (IsTeacherInformationFilled())
                {
                    npcQuestions.Teacher = new NPCQuestionsInformation();
                    npcQuestions.Teacher.Questions = questions.ToArray();
                    npcQuestions.Teacher.AmountQuestionsForTest = int.Parse(amountQuestionsForTestTeacherInput.text);
                    npcQuestions.MinPercentCorrectTeacher = int.Parse(minPercentCorrectTeacherInput.text);
                    npcQuestions.MinPercentCorrectToUnlockTeacher = int.Parse(minPercentCorrectToUnlockTeacherInput.text);
                    FileEncryption.WriteFile("..\\The Labyrinth of GSTU\\Assets\\Resources\\Questions\\" + topicNameInput.text + ".json", npcQuestions);
                    ClearAllContent(true);
                }
                break;
        }
    }

    private void ClearAllContent(bool isPanelStudent)
    {
        questions = new List<Question>();
        answerInputButtons = new List<TMP_InputField>();
        panelStudent.SetActive(isPanelStudent);
        panelTeacher.SetActive(!isPanelStudent);
        buttonUpdateQuestion.onClick.AddListener(null);
        ClearInputContent();
        ClearButtonsPanel(panelChangeAnswers);
    }

    private bool IsStudentInformationFilled()
    {
        if (topicNameInput.text == string.Empty)
        {
            Debug.Log("Введите название темы");
            return false;
        }
        if (amountStudentsOnFloorInput.text == string.Empty)
        {
            Debug.Log("Введите количество студентов на этаже");
            return false;
        }
        if (amountQuestionsForTestInput.text == string.Empty)
        {
            Debug.Log("Введите количество вопросов у каждого студента");
            return false;
        }
        if (questions.Count == 0)
        {
            Debug.Log("Создайте хотя бы один вопрос");
            return false;
        }
        return true;
    }

    private bool IsTeacherInformationFilled()
    {
        if (amountQuestionsForTestTeacherInput.text == string.Empty)
        {
            Debug.Log("Введите количество вопросов в тесте");
            return false;
        }
        if (minPercentCorrectTeacherInput.text == string.Empty)
        {
            Debug.Log("Введите минимальное количество процентов для выполнения теста");
            return false;
        }
        if (minPercentCorrectToUnlockTeacherInput.text == string.Empty)
        {
            Debug.Log("Введите минимальное количество процентов для получения доступа к преподавателю");
            return false;
        }
        if (questions.Count == 0)
        {
            Debug.Log("Создайте хотя бы один вопрос");
            return false;
        }
        return true;
    }

    private GameObject CreateChangeButton(Button button, GameObject panel = null)
    {
        Button createdButton = Instantiate(button);
        if (panel != null)
            createdButton.transform.SetParent(panel.transform);
        createdButton.transform.localScale = button.transform.localScale;
        createdButton.transform.position = button.transform.position;
        return createdButton.gameObject;
    }

    private void AddComponentsToButton(ChangeQuestionButton button, Question question, string buttonName)
    {
        button.Button.onClick.RemoveAllListeners();
        button.ButtonText.text = "Изменить: " + buttonName;
        button.Button.onClick.AddListener(() => DemonstrateOfCreatedQuestion(question));
        button.DeleteButton.onClick.AddListener(() => DeleteQuestion(question, button.gameObject));
    }

    public void DeleteQuestion(Question question, params GameObject[] buttons)
    {
        questions.Remove(question);
        foreach (var button in buttons)
            Destroy(button);
    }

    public void UpdateQuestion(Question question, ChangeQuestionButton button)
    {
        for (int i = 0; i < questions.Count; i++)
        {
            if (questions[i] == question)
            {
                if (CheckOnNullParameters())
                    return;
                string[] answers = GetAnswers();
                questions[i] = GetQuestion(answers);
                AddComponentsToButton(button, questions[i], inputEnteringQuestion.text);
                return;
            }
        }
        Debug.Log("Вопрос не был выбран");
    }

    private bool CheckOnNullParameters()
    {
        if (inputEnteringQuestion.text == string.Empty)
        {
            Debug.Log("Введите вопрос");
            return true;
        }

        if (correctAnswerInput.text == string.Empty)
        {
            Debug.Log("Не был введен ответ на вопрос.");
            return true;
        }

        if (questionTimeButton.text == string.Empty)
        {
            Debug.Log("Не было назначено время для ответа на вопрос");
            return true;
        }

        if (pointsForCorrectAnswerInput.text == string.Empty || pointsForWrongAnswerInput.text == string.Empty)
        {
            Debug.Log("Не была назначена стоимость вопроса");
            return true;
        }
        return false;
    }

    private string[] GetAnswers()
    {
        if (answerInputButtons.Count == 0)
            return new string[1];

        string[] answers = new string[answerInputButtons.Count];
        bool isCorrectAnswer = false;

        for (int i = 0; i < answerInputButtons.Count; i++)
        {
            if (answerInputButtons[i].text == string.Empty)
            {
                Debug.Log("Ответ не был заполнен");
                return null;
            }
            answers[i] = answerInputButtons[i].text;
            if (answerInputButtons[i].text == correctAnswerInput.text)
                isCorrectAnswer = true;
        }
        if (!isCorrectAnswer)
        {
            Debug.Log("Верный ответ не найден в вариантах ответа");
            return null;
        }
        return answers;
    }


    private Question GetQuestion(string[] answers)
    {
        Question question = new Question();
        question.QuestionText = inputEnteringQuestion.text;
        question.QuestionTime = int.Parse(questionTimeButton.text);
        question.Answers = answers;
        question.CorrectAnswer = correctAnswerInput.text;
        question.PointsForCorrectAnswer = int.Parse(pointsForCorrectAnswerInput.text);
        question.PointsForWrongAnswer = int.Parse(pointsForWrongAnswerInput.text);
        return question;
    }

    private void ClearButtonsPanel(GameObject panel)
    {
        foreach (var buttons in panel.GetComponentsInChildren<Transform>())
        {
            if (buttons.gameObject != panel)
                Destroy(buttons.gameObject);
        }
    }

    private void ClearInputContent()
    {
        inputEnteringQuestion.text = string.Empty;
        correctAnswerInput.text = string.Empty;
        answerInputButtons = new List<TMP_InputField>();
        ClearButtonsPanel(panelAnswers);
    }

    private void DemonstrateOfCreatedQuestion(Question question)
    {
        ClearButtonsPanel(panelAnswers);
        answerInputButtons = new List<TMP_InputField>();
        buttonUpdateQuestion.onClick.RemoveAllListeners();
        inputEnteringQuestion.text = question.QuestionText;
        questionTimeButton.text = question.QuestionTime.ToString();
        correctAnswerInput.text = question.CorrectAnswer;
        pointsForCorrectAnswerInput.text = question.PointsForCorrectAnswer.ToString();
        pointsForWrongAnswerInput.text = question.PointsForWrongAnswer.ToString();
        foreach (var answer in question.Answers)
            AddAnswerButton(answer);

        ChangeQuestionButton changeQuestionButton = EventSystem.current.currentSelectedGameObject.GetComponent<ChangeQuestionButton>();
        buttonUpdateQuestion.onClick.AddListener(() => UpdateQuestion(question, changeQuestionButton));
    }
}
