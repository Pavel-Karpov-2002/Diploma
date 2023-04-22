using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static AnswerButton;

public class TestingAnswersScript : DialogScript
{
    [SerializeField] private GameObject buttonsPanel;

    private static TestingAnswersScript instance;

    private void Start()
    {
        if (instance == null)
            instance = this;

        AnswerButton.OnPlayerAnswered += ChangeInterectableAtButtons;
    }

    public override void WriteQuestion(int numQuestion)
    {
        foreach (var buttons in buttonsPanel.GetComponentsInChildren<Transform>())
        {
            if (buttons.gameObject != buttonsPanel)
                Destroy(buttons.gameObject);
        }

        questionText.font = DialogParameters.QuestionFontAsset;
        questionText.text = Questions[numQuestion].questionText;

        int answer = Convert.ToInt32(Questions[numQuestion].correctAnswer);

        for (int i = 0; i < Questions[0].answers.Length; i++)
        {
            CreateButtonAnswers(Questions[numQuestion].answers[i], answer - 1 == i);
        }
    }

    private void CreateButtonAnswers(string answer, bool isCorrect)
    {
        GameObject button = Instantiate(DialogParameters.ButtonAnswer, buttonsPanel.transform);
        button.GetComponentInChildren<TextMeshProUGUI>().font = DialogParameters.ButtonFontAsset;
        button.GetComponentInChildren<TextMeshProUGUI>().text = answer;
        button.GetComponent<Button>().onClick.AddListener(() => button.GetComponent<AnswerButton>().AnswerTheQuestion(answer, DialogParameters.TimeAfterResponse));
        
        if (isCorrect)
            button.GetComponent<AnswerButton>().Answer = answer;
    }

    private void ChangeInterectableAtButtons(bool isCorrect)
    {
        foreach (var button in buttonsPanel.GetComponentsInChildren<Button>())
        {
            if (button.gameObject != buttonsPanel)
                button.interactable = false;
        }
    }

    public static new TestingAnswersScript GetInstance()
    {
        if (instance == null)
            instance = Resources.FindObjectsOfTypeAll<TestingAnswersScript>()[0];

        return instance;
    }

    private void OnDestroy()
    {
        instance = null;
        AnswerButton.OnPlayerAnswered -= ChangeInterectableAtButtons;
    }
}
