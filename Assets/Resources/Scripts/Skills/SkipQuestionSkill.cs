using UnityEngine;

[CreateAssetMenu(fileName = "SkipQuestionSkill.Asset", menuName = "Skills/SkipQuestionSkill")]
public class SkipQuestionSkill : Skill
{
    private int amountSkipQuestions;

    public override string SkillInformation => "���� �������, ������� ������, ��� ���� �������� �������� ������� ���� �� ����� ������." +
        "<br><br><b>������� ���������� ������.</b><br>";

    public override string SkillName => "���������� ��������";

    public SkipQuestionSkill(int amountSkipQuestions) : base()
    {
        this.amountSkipQuestions = amountSkipQuestions;
    }

    public override void Activate()
    {
        if (amountSkipQuestions < 0)
            return;

        DialogScript.GetInstance().ShowNewQuestion();
        amountSkipQuestions--;
    }

    public override void InitializeSkill()
    {
        PlayerSkills.GetPassiveSkills += CreateSkills.CreateSkipQuestionSkill;
    }
}
