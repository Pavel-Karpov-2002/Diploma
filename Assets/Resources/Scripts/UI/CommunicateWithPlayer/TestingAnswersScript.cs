using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestingAnswersScript : MonoBehaviour, IWriteQuestion
{
    [SerializeField] private DialogParameters dialogParameters;
    [SerializeField] private GameObject buttonsPanel;
    [SerializeField] public TextMeshProUGUI questionText;

    public GameObject ButtonsPanel => buttonsPanel;

    private void OnEnable()
    {
        AnswerButton.OnPlayerAnswered += ChangeInterectableAtButtons;
    }

    public void WriteQuestion(Question question)
    {
        ChangePanelScript.ClearPanel(buttonsPanel);
        questionText.text = question.QuestionText;
        AddButtonsToPanel(question);
    }

    private void AddButtonsToPanel(Question question)
    {
        HashSet<int> answerUsed = new HashSet<int>();
        for (int i = 0; i < question.Answers.Length; i++)
        {
            int rnd = GetRandomNumber.GenerateRandomNumberNotUsed(0, question.Answers.Length, answerUsed);
            answerUsed.Add(rnd);
            CreateButtonAnswers(question, question.Answers[rnd]);
        }
    }

    private void CreateButtonAnswers(Question question, string answer)
    {
        GameObject button = Instantiate(dialogParameters.ButtonAnswer, buttonsPanel.transform);
        button.GetComponentInChildren<TextMeshProUGUI>().text = answer;
        button.GetComponent<Button>().onClick.AddListener(() => button.GetComponent<AnswerButton>().AnswerTheQuestion(question, answer));
        button.GetComponent<AnswerButton>().Answer = question.CorrectAnswer;
    }

    private void ChangeInterectableAtButtons(int change)
    {
        foreach (var button in buttonsPanel.GetComponentsInChildren<Button>())
        {
            if (button.gameObject != buttonsPanel)
                button.interactable = false;
        }
    }

    private void OnDisable()
    {
        AnswerButton.OnPlayerAnswered -= ChangeInterectableAtButtons;
    }
}
