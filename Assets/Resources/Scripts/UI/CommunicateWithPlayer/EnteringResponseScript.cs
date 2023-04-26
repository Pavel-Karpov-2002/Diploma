using System.Collections;
using TMPro;
using UnityEngine;

public class EnteringResponseScript : DialogScript
{
    [SerializeField] private TMP_InputField enteredText;
    [SerializeField] private AnswerButton answerButton;

    private static EnteringResponseScript instance;
    private int numQuestion;

    public AnswerButton AnswerButton => answerButton;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    private void OnEnable()
    {
        StartCoroutine(SetAnswer());
    }

    private IEnumerator SetAnswer()
    {
        yield return new WaitForSeconds(0.5f);
        answerButton.Answer = Questions[numQuestion].correctAnswer;
    }

    public void ClickToCompareAnswer()
    {
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
