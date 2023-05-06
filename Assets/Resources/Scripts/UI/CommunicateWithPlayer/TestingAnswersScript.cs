using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestingAnswersScript : DialogScript
{
    [SerializeField] private GameObject buttonsPanel;

    public GameObject ButtonsPanel => buttonsPanel;

    private void OnEnable()
    {
        AnswerButton.OnPlayerAnswered += ChangeInterectableAtButtons;
    }

    public override void WriteQuestion(Question question)
    {
        ClearButtonsPanel();
        questionText.text = question.QuestionText;
        AddButtonToPanel(question);
    }

    private void ClearButtonsPanel()
    {
        foreach (var buttons in buttonsPanel.GetComponentsInChildren<Transform>())
        {
            if (buttons.gameObject != buttonsPanel)
                Destroy(buttons.gameObject);
        }
    }

    private void AddButtonToPanel(Question question)
    {
        for (int i = 0; i < question.Answers.Length; i++)
        {
            CreateButtonAnswers(question, question.Answers[i]);
        }
    }

    private void CreateButtonAnswers(Question question, string answer)
    {
        GameObject button = Instantiate(DialogParameters.ButtonAnswer, buttonsPanel.transform);
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
