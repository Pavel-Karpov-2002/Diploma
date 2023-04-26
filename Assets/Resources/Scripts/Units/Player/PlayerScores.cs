public class PlayerScores : PlayerConstructor
{
    private int scores;
    private static int recordScores;

    public int Scores 
    { 
        get { return scores; } 
        private set 
        {  
            scores = value;

            ScoresUI.GetInstance().ChangeScoresText(scores);

            if (scores > recordScores)
                recordScores = scores;
        }
    }

    public int NumberOfPointsPerQuestions { get; set; }

    public static int RecordScores => recordScores;

    private void Start()
    {
        if (NumberOfPointsPerQuestions == 0)
            NumberOfPointsPerQuestions = PlayerParameters.MinPointsForQuestion;
    }

    public void ChangeScores(bool isCorrectAnswer)
    {
        Scores += NumberOfPointsPerQuestions * (isCorrectAnswer ? 1 : -1);
    }

    public void ChangeScores(int countPoints)
    {
        Scores += countPoints;
    }
}
