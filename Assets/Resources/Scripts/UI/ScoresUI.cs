using TMPro;
using UnityEngine;

public class ScoresUI : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI scoresText;
    [SerializeField] private DialogParameters dialogParameters;

    private static string textScores = "Ваши баллы: ";
    private static ScoresUI instance;

    private void Start()
    {
        if (instance == null)
            instance = this;
        scoresText.text = textScores + PlayerScores.GetInstance().Scores.ToString();
        scoresText.font = dialogParameters.QuestionFontAsset;
    }

    public void ChangeScoresText(int scores)
    {
        instance.scoresText.text = textScores + scores.ToString();
    }

    public static ScoresUI GetInstance()
    {
        if (instance == null)
            instance = Resources.FindObjectsOfTypeAll<ScoresUI>()[0];

        return instance;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
