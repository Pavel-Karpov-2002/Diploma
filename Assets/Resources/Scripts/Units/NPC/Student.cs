using UnityEngine;

public class Student : NPC
{
    [SerializeField] private GameObject warn;

    public NPCQuestionsInformation NPCQuestionsInformation { get; set; }

    public override void SetExpectation(bool expectation)
    {
        warn.SetActive(expectation);
        IsExpectation = expectation;
        AnswerButton.ChangeColorButton = true;
        AnswerButton.OnPlayerAnswered += ChangeScores;
        DialogScript.NpcType = NPCType.Student;
        NewQuestions();
    }

    public void ChangeScores(int countPoints)
    {
        PlayerScores.GetInstance().ChangeScores(countPoints);
    }
}
