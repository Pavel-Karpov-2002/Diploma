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
    public event ChangeButtonColor ChangeButton;
    private IEnumerator nextQuestionCoroutine;
    private bool coroutineIsNotProcessed = true;
    public static bool isSetChangeColor = true;

    private void OnEnable()
    {
        if (isActiveAndEnabled)
            thisButton.interactable = true;
    }

    public void AnswerTheQuestion(string answer, float timeAfterQuestion)
    {
        ChangeColorButton(gameParameters.Dialog.ButtonFalseColor);

        bool isCorrect = CheckCorrectAnswer(answer);

        if (isCorrect)
            ChangeColorButton(gameParameters.Dialog.ButtonTrueColor);
        
        if (coroutineIsNotProcessed)
        {
            nextQuestionCoroutine = (NextQuestion(timeAfterQuestion));
            StartCoroutine(nextQuestionCoroutine);
        }

        if (OnPlayerAnswered != null)
            OnPlayerAnswered?.Invoke(isCorrect);

        TimerDialogScript.GetInstance().IsPaused = true;
        thisButton.interactable = false;
    }

    private void ChangeColorButton(Color color)
    {
        if (!isSetChangeColor)
        {
            ChangeButton = null;
            return;
        }

        ChangeButton = ChangeColor;

        if (ChangeButton != null)
            ChangeButton.Invoke(color);
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

        ChangeButton = null;
        coroutineIsNotProcessed = true;
    }

    public void ChangeColor(Color color)
    {
        buttonImage.color = color;
    }
}
