using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnteringResponseScript : DialogScript
{
    [SerializeField] private TMP_InputField enteredText;
    [SerializeField] private AnswerButton answerButton;


    private static EnteringResponseScript instance;
    private int numQuestion;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void ClickToCompareAnswer()
    {
        answerButton.Answer = Questions[numQuestion].correctAnswer;
        answerButton.AnswerTheQuestion(enteredText.text, DialogParameters.TimeAfterResponse);
    }

    public override void WriteQuestion(int numQuestion)
    {
        enteredText.text = string.Empty;
        answerButton.ButtonImage.color = Color.white;
        answerButton.Button.interactable = true;
        this.numQuestion = numQuestion;
        questionText.font = DialogParameters.QuestionFontAsset;
        questionText.text = Questions[numQuestion].questionText;
    }

    public static new EnteringResponseScript GetInstance()
    {
        if (instance == null)
            instance = Resources.FindObjectsOfTypeAll<EnteringResponseScript>()[0];

        return instance;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
