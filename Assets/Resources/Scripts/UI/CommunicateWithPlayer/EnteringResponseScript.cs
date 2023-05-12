using TMPro;
using UnityEngine;

public class EnteringResponseScript : MonoBehaviour, IWriteQuestion
{
    [SerializeField] public TextMeshProUGUI questionText;
    [SerializeField] private TMP_InputField enteredText;
    [SerializeField] private AnswerButton answerButton;

    public AnswerButton AnswerButton => answerButton;


    public void ClickToCompareAnswer(Question question)
    {
        answerButton.AnswerTheQuestion(question, enteredText.text);
    }

    public void WriteQuestion(Question question)
    {
        answerButton.Button.onClick.RemoveAllListeners();
        enteredText.text = string.Empty;
        answerButton.Answer = question.CorrectAnswer;
        answerButton.ButtonImage.color = Color.white;
        answerButton.Button.interactable = true;
        answerButton.Button.onClick.AddListener(() => ClickToCompareAnswer(question));
        questionText.text = question.QuestionText;
    }
}
