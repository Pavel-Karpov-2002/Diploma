using System;
using TMPro;
using UnityEngine;

public class TestingAnswersScript : DialogScript
{
    [SerializeField] private GameObject buttonsPanel;
    
    public override void WriteQuestion(int numQuestion)
    {
        questionText.font = DialogParameters.QuestionFontAsset;
        questionText.text = Questions[numQuestion].questionText;
        int answer = Convert.ToInt32(Questions[numQuestion].correctAnswer);
        for (int i = 0; i < Questions[0].answers.Length; i++)
        {
            if (answer - 1 != i)
            {
                CreateButtonAnswers(Questions[numQuestion].answers[i]);
            }
            else
            {
                CreateButtonAnswers(Questions[numQuestion].answers[i], true);
            }
        }
        gameObject.SetActive(true);
    }

    private void CreateButtonAnswers(string answer)
    {
        GameObject button = Instantiate(gameParameters.Dialog.ButtonAnswer, buttonsPanel.transform);
        button.GetComponentInChildren<TextMeshProUGUI>().font = DialogParameters.ButtonFontAsset;
        button.GetComponentInChildren<TextMeshProUGUI>().text = answer;
    }

    private void CreateButtonAnswers(string answer, bool isCorrect)
    {
        GameObject button = Instantiate(gameParameters.Dialog.ButtonAnswer, buttonsPanel.transform);
        button.GetComponentInChildren<TextMeshProUGUI>().font = DialogParameters.ButtonFontAsset;
        button.GetComponentInChildren<TextMeshProUGUI>().text = answer;
        button.GetComponent<AnswerButton>().Valid = isCorrect;
    }
}
