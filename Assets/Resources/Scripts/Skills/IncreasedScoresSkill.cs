using UnityEngine;

[CreateAssetMenu(fileName = "IncreasedScoresSkill.Asset", menuName = "Skills/IncreasedScoresSkill")]
public class IncreasedScoresSkill : Skill
{
    private int amountPoints;

    public override string SkillInformation => "� ������� ������� ��� ������, ������� ����������� ����, ������� �������� ��� ����� ��������� ���� ������, ���������� �� �� ������." +
        "<br><br><b>������� ���� �������������� ����� �� ������ ������ �� �������.</b><br>";

    public override string SkillName => "���� ������";

    public IncreasedScoresSkill(int amountPoints) : base()
    {
        this.amountPoints = amountPoints;
    }

    public override void Activate()
    {
        ((PlayerScores)PlayerConstructor.GetInstance()).NumberOfPointsForCorrectAnswer += amountPoints;
    }

    public override void InitializeSkill()
    {
        PlayerSkills.GetPassiveSkills += CreateSkills.CreateIncreasedScoresSkill;
    }
}
