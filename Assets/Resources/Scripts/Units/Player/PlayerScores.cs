using UnityEngine;

public class PlayerScores : PlayerConstructor
{
    [SerializeField] private PlayerLoseScript playerLose;

    private int scores;
    private static int recordScores;

    public int Scores 
    { 
        get { return scores; } 
        set 
        {  
            scores = value;

            if (scores < 0)
            {
                playerLose.PlayerLosed();
                return;
            }

            ScoresUI.GetInstance().ChangeScoresText(scores);

            if (scores > recordScores)
                recordScores = scores;
        } 
    }

    public static int RecordScores => recordScores;

    private void Start()
    {
        AnswerButton.OnPlayerAnswered += ChangeScores;
    }

    private void ChangeScores(bool isCorrectAnswer)
    {
        Scores += PlayerParameters.MinPointsForQuestion * (isCorrectAnswer ? 1 : -1);
    }

    private void OnDestroy()
    {
        AnswerButton.OnPlayerAnswered -= ChangeScores;
    }
}
