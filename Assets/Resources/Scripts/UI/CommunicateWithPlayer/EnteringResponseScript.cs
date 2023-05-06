using TMPro;
using UnityEngine;

public class EnteringResponseScript : DialogScript
{
    [SerializeField] private TMP_InputField enteredText;
    [SerializeField] private AnswerButton answerButton;

    public AnswerButton AnswerButton => answerButton;

    public void ClickToCompareAnswer(Question question)
    {
        answerButton.AnswerTheQuestion(question, enteredText.text);
    }

    public override void WriteQuestion(Question question)
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
