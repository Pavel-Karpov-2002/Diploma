using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    [SerializeField] protected GameParameters gameParameters;
    [SerializeField] private Image buttonImage;
    [SerializeField] private Button thisButton;

    public string Answer { get; set; }
    public Button Button => thisButton;
    public Image ButtonImage => buttonImage;

    public delegate void ChangeButtonColor(Color color);
    public delegate void PlayerAnswered(bool isCorrect);

    public static event PlayerAnswered OnPlayerAnswered;
    public event ChangeButtonColor ChangeAnswerButton;

    private IEnumerator nextQuestionCoroutine;
    private bool coroutineIsNotProcessed = true;
    private static bool isSetChangeColor = true;

    public static bool ChangeColorButton { get => isSetChangeColor; set => isSetChangeColor = value; }

    private void OnEnable()
    {
        if (isActiveAndEnabled)
            thisButton.interactable = true;
    }

    public void AnswerTheQuestion(string answer, float timeAfterQuestion)
    {
        ChangeButton(gameParameters.Dialog.ButtonFalseColor);

        bool isCorrect = CheckCorrectAnswer(answer);

        if (isCorrect)
            ChangeButton(gameParameters.Dialog.ButtonTrueColor);
        
        if (coroutineIsNotProcessed)
        {
            nextQuestionCoroutine = NextQuestion(timeAfterQuestion);
            StartCoroutine(nextQuestionCoroutine);
        }

        if (OnPlayerAnswered != null)
            OnPlayerAnswered?.Invoke(isCorrect);

        TimerDialogScript.GetInstance().IsPaused = true;
        thisButton.interactable = false;
    }

    private void ChangeButton(Color color)
    {
        if (!isSetChangeColor)
        {
            ChangeAnswerButton = null;
            return;
        }

        ChangeAnswerButton = ChangeColor;

        if (ChangeAnswerButton != null)
            ChangeAnswerButton.Invoke(color);
    }

    private bool CheckCorrectAnswer(string answer)
    {
        return string.Compare(answer, Answer, StringComparison.OrdinalIgnoreCase) == 0;
    }

    private IEnumerator NextQuestion(float timeAfterQuestion)
    {
        coroutineIsNotProcessed = false;
        yield return new WaitForSeconds(timeAfterQuestion);

        DialogScript.GetInstance().ShowNewQuestion();

        ChangeAnswerButton = null;
        coroutineIsNotProcessed = true;
    }

    public void ChangeColor(Color color)
    {
        buttonImage.color = color;
    }

    public static void ResetOnPlayerAnswered() => OnPlayerAnswered = null;
}
