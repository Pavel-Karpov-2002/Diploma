using UnityEngine;

public class Student : NPC
{
    [SerializeField] private GameObject warn;

    private void Start()
    {
        SetSkin();
    }

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

    protected override void SetSkin()
    {
        int rnd = Random.Range(0, npcParameters.Students.Count - 1);
        npcSkin.sprite = npcParameters.Students[rnd].Skin;
        npcAnimator.runtimeAnimatorController = npcParameters.Students[Random.Range(0, npcParameters.Students.Count - 1)].SkinAnimator;
    }
}
