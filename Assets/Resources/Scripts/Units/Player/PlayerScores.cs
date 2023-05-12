using UnityEngine;

public class PlayerScores : CustomSingleton<PlayerScores>
{
    [SerializeField] private GameParameters gameParameters;
    private int scores;

    public int Scores 
    {
        get { return scores; }
        private set 
        {  
            scores = value;
            if (scores < 0)
            {
                SceneChangeScript.GetInstance().ChangeScene(gameParameters.LobbySceneName);
                return;
            }
            ScoresUI.Instance.ChangeScoresText(scores);
        }
    }

    public float AdditionalPointsInPercentage { get; set; }

    public void ChangeScores(int countPoints)
    {
        if (countPoints > 0)
            Scores += (int)(countPoints * AdditionalPointsInPercentage);
        Scores += countPoints;
    }
}
