using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[Serializable]
public class Question
{
    [JsonProperty("questionText")]
    public string QuestionText { get; set; }

    [JsonProperty("answers")]
    public string[] Answers { get; set; }

    [JsonProperty("questionTime")]
    public int QuestionTime { get; set; }

    [JsonProperty("correctAnswer")]
    public string CorrectAnswer { get; set; }

    [JsonProperty("pointsForCorrectAnswer")]
    public int PointsForCorrectAnswer { get; set; }

    [JsonProperty("pointsForWrongAnswer")]
    public int PointsForWrongAnswer { get; set; }

    public bool IsTest()
    {
        return Answers.Length > 1;
    }

    public override bool Equals(object obj)
    {
        return obj is Question question &&
               QuestionText == question.QuestionText &&
               EqualityComparer<string[]>.Default.Equals(Answers, question.Answers) &&
               QuestionTime == question.QuestionTime &&
               CorrectAnswer == question.CorrectAnswer;
    }

    public bool Equals(string other)
    {
        if (string.Compare(CorrectAnswer, other, StringComparison.CurrentCulture) == 0) return true;
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(QuestionText, Answers, QuestionTime, CorrectAnswer);
    }

    public override string ToString()
    {
        return "Question: " + QuestionText + ";\n"
            + "Answers count: " + Answers.Length + ";\n"
            + "Time to question: " + QuestionTime + ";\n"
            + "Correct answer: " + CorrectAnswer + ";\n"
            + "Points for wrong answer: " + PointsForWrongAnswer + ";\n"
            + "Points for correct answer: " + PointsForCorrectAnswer + ";\n";
    }
}

