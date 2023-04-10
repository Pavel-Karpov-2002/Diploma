using System;
using TMPro;
using UnityEngine;

public class EnteringReponseScript : DialogScript
{
    [SerializeField] private TextMeshProUGUI enteredText;
    [SerializeField] private AnswerButton answerButton;

    private int numQuestion;

    private void Start()
    {
        WriteQuestion(1);
    }

    public void ClickToCompareAnswer()
    {
        if (string.Compare(Questions[numQuestion].correctAnswer.Trim(), enteredText.text.Remove(enteredText.text.Length - 1, 1), StringComparison.OrdinalIgnoreCase) == 0)
        {
            answerButton.Valid = true;
        }
        else
        {
            answerButton.Valid = false;
        }
        answerButton.ClickToAnswer();
    }

    public override void WriteQuestion(int numQuestion)
    {
        this.numQuestion = numQuestion;
        questionText.font = DialogParameters.QuestionFontAsset;
        questionText.text = Questions[numQuestion].questionText;
        gameObject.SetActive(true);
    }
}
