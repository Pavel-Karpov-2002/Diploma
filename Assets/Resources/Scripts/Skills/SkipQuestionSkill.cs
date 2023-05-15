using UnityEngine;

[CreateAssetMenu(fileName = "SkipQuestionSkill.Asset", menuName = "CustomParameters/Skills/SkipQuestionSkill")]
public class SkipQuestionSkill : Skill
{
    private int amountUses;

    public int AmountUses => amountUses;

    public SkipQuestionSkill(int amountUses) : base()
    {
        this.amountUses = amountUses;
    }

    public override void Activate()
    {
        if (amountUses < 0)
            return;
        DialogScript.Instance.ShowNewQuestion();
        amountUses--;
    }

    public override void InitializeSkill()
    {
        PlayerSkills.GetActiveSkills += CreateSkills.CreateSkipQuestionSkill;
    }
}
