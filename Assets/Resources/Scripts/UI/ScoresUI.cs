using TMPro;
using UnityEngine;

public class ScoresUI : Singleton<ScoresUI>
{
    [SerializeField] protected TextMeshProUGUI scoresText;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private DialogParameters dialogParameters;

    private void Start()
    {
        scoresText.text = PlayerScores.Instance.Scores.ToString();
        scoresText.font = dialogParameters.QuestionFontAsset;
    }

    public void ChangeScoresText(int scores)
    {
        scoresText.text = scores.ToString();
    }

    public void ChangeAlphaPanel(int alpha)
    {
        canvasGroup.alpha = alpha;
    }
}
