using TMPro;
using UnityEngine;

public class ScoresUI : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI scoresText;
    [SerializeField] private GameParameters gameParameters;

    private static string textScores = "Ваши баллы: ";
    private static ScoresUI instance;

    private void Start()
    {
        if (instance == null)
            instance = this;
        scoresText.text = textScores + "0";
        scoresText.font = gameParameters.Dialog.QuestionFontAsset;
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
