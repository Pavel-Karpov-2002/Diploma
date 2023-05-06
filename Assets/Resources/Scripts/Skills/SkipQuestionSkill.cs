using UnityEngine;

[CreateAssetMenu(fileName = "SkipQuestionSkill.Asset", menuName = "CustomParameters/Skills/SkipQuestionSkill")]
public class SkipQuestionSkill : Skill
{
    private int amountSkipQuestions;

    public override string SkillInformation => "Есть легенда, которая гласит, что этот кристалл помогает сбежать даже от самой смерти." +
        "<br><br><b>Предмет пропускает вопрос.</b><br>";

    public override string SkillName => "Магический кристалл";

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
