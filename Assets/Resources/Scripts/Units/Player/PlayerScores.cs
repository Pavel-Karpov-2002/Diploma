using UnityEngine;

public class PlayerScores : PlayerConstructor
{
    [SerializeField] private PlayerLoseScript playerLose;
    private static int scores;
    private static int recordScores;

    public int Scores 
    { 
        get { return scores; } 
        private set 
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

    public int NumberOfPointsForCorrectAnswer { get; set; }

    public int NumberOfPointsForWrongAnswer { get; set; }

    public static int RecordScores => recordScores;

    private void Start()
    {
        if (NumberOfPointsForCorrectAnswer == 0)
            NumberOfPointsForCorrectAnswer = PlayerParameters.MinPointsForCorrectAnswer;

        if (NumberOfPointsForWrongAnswer == 0)
            NumberOfPointsForWrongAnswer = PlayerParameters.MinPointsForWrongAnswer;
    }

    public void ChangeScores(bool isCorrectAnswer)
    {
        Scores += (isCorrectAnswer ? NumberOfPointsForCorrectAnswer : -NumberOfPointsForWrongAnswer);
    }

    public void ChangeScores(int countPoints)
    {
        Scores += countPoints;
    }
}
