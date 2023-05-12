using TMPro;
using UnityEngine;

public class ScoresUI : CustomSingleton<ScoresUI>
{
    [SerializeField] protected TextMeshProUGUI scoresText;
    [SerializeField] private DialogParameters dialogParameters;

    private static string textScores = "Ваши баллы: ";

    private void Start()
    {
        scoresText.text = textScores + PlayerScores.Instance.Scores.ToString();
        scoresText.font = dialogParameters.QuestionFontAsset;
    }

    public void ChangeScoresText(int scores)
    {
        scoresText.text = textScores + scores.ToString();
    }
}
