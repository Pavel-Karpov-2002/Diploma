using System;
using System.Collections.Generic;
using System.ComponentModel;

[Serializable]
public class Question
{
    public string questionText;
    public string[] answers;
    public int questionTime;
    public string correctAnswer;
    public QuestionType questionType;

    public override bool Equals(object obj)
    {
        return obj is Question question &&
               questionText == question.questionText &&
               EqualityComparer<string[]>.Default.Equals(answers, question.answers) &&
               questionTime == question.questionTime &&
               correctAnswer == question.correctAnswer;
    }

    public bool Equals(string other)
    {
        if (string.Compare(correctAnswer, other, StringComparison.CurrentCulture) == 0) return true;
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(questionText, answers, questionTime, correctAnswer);
    }

    public override string ToString()
    {
        return "Question: " + questionText + ";\n"
            + "Answers count: " + answers.Length + ";\n"
            + "Time to question: " + questionTime + ";\n"
            + "Correct answer: " + correctAnswer + ";\n"
            + "Type: " + questionType;
    }

    public enum QuestionType
    {
        [Description("test")]
        Test,
        [Description("entering")]
        Entering
    }
}

