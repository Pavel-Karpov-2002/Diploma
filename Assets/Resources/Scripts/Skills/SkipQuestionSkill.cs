using UnityEngine;

[CreateAssetMenu(fileName = "SkipQuestionSkill.Asset", menuName = "CustomParameters/Skills/SkipQuestionSkill")]
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

        DialogPanelSingleton.GetInstance().ShowNewQuestion();
        amountSkipQuestions--;
    }

    public override void InitializeSkill()
    {
        PlayerSkills.GetPassiveSkills += CreateSkills.CreateSkipQuestionSkill;
    }
}
