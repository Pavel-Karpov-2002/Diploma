using UnityEngine;

[CreateAssetMenu(fileName = "GiveAdditionalQuestionSkill.Asset", menuName = "Skills/GiveAdditionalQuestionSkill")]
public class GiveAdditionalQuestionSkill : Skill
{
    private int amountAdditionalQuestions;
    public override string SkillInformation => "� ����� ������� ������ ��� ������. ���-�� ��� �� �������� � ����� ������� ��������, ������� ������� �� ������� ������� �������, ������� ���������� ��� ������ ������ �� ��� �� ����� �����." +
        "<br><br><b>������� ��������� �������������� ������� � �����.</b><br>";

    public override string SkillName => "����� ������";

    public GiveAdditionalQuestionSkill(int amountAdditionalQuestions) : base()
    {
        this.amountAdditionalQuestions = amountAdditionalQuestions;
    }


    public override void Activate()
    {
        DialogScript.GetInstance().AmountQuestionPerOneNPC += amountAdditionalQuestions;
    }

    public override void InitializeSkill()
    {
        PlayerSkills.GetPassiveSkills += CreateSkills.CreateGiveAdditionalQuestionSkill;
    }
}
