using UnityEngine;

public class PlayerScores : PlayerConstructor
{
    [SerializeField] private GameParameters gameParameters;
    private int scores;

    public static PlayerScores instance;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

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

            ScoresUI.GetInstance().ChangeScoresText(scores);
        }
    }

    public float AdditionalPointsInPercentage { get; set; }

    public void ChangeScores(int countPoints)
    {
        if (countPoints > 0)
            Scores += (int)(countPoints * AdditionalPointsInPercentage);
        Scores += countPoints;
    }

    public static PlayerScores GetInstance()
    {
        if (instance == null)
            instance = Resources.FindObjectsOfTypeAll<PlayerScores>()[0];

        return instance;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
