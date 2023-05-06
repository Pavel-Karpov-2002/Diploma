using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    [SerializeField] protected DialogParameters dialogParameters;
    [SerializeField] private Image buttonImage;
    [SerializeField] private Button thisButton;

    public string Answer { get; set; }

    public Button Button => thisButton;
    public Image ButtonImage => buttonImage;

    public delegate void ChangeButtonColor(Color color);
    public delegate void PlayerAnswered(int countPoints);

    public static event PlayerAnswered OnPlayerAnswered;
    public event ChangeButtonColor ChangeAnswerButton;

    private IEnumerator nextQuestionCoroutine;
    private bool coroutineIsNotProcessed = true;

    public static bool ChangeColorButton = true;

    private void OnEnable()
    {
        if (isActiveAndEnabled)
            thisButton.interactable = true;
        ChangeAnswerButton = ChangeColor;
    }

    public void AnswerTheQuestion(Question question, string answer)
    {
        bool isCorrect = CheckCorrectAnswer(answer);
        ChangeButton(isCorrect ? dialogParameters.ButtonTrueColor : dialogParameters.ButtonFalseColor);

        if (coroutineIsNotProcessed)
        {
            nextQuestionCoroutine = NextQuestion(dialogParameters.TimeAfterResponse);
            StartCoroutine(nextQuestionCoroutine);
        }

        if (OnPlayerAnswered != null)
            OnPlayerAnswered?.Invoke(isCorrect ? question.PointsForCorrectAnswer : -question.PointsForWrongAnswer);

        TimerDialogScript.GetInstance().IsPaused = true;
        thisButton.interactable = false;
    }

    private void ChangeButton(Color color)
    {
        if (!ChangeColorButton)
            return;
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

        DialogPanelSingleton.GetInstance().ShowNewQuestion();

        coroutineIsNotProcessed = true;
    }

    public void ChangeColor(Color color)
    {
        buttonImage.color = color;
    }

    public static void ResetOnPlayerAnswered() => OnPlayerAnswered = null;
}
