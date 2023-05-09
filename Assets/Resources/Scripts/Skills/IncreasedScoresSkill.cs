using UnityEngine;

[CreateAssetMenu(fileName = "IncreasedScoresSkill.Asset", menuName = "CustomParameters/Skills/IncreasedScoresSkill")]
public class IncreasedScoresSkill : Skill
{
    private float amountPoints;

    public IncreasedScoresSkill(float amountPoints) : base()
    {
        this.amountPoints = amountPoints;
    }

    public override void Activate()
    {
        PlayerScores.GetInstance().AdditionalPointsInPercentage = (amountPoints / 100f);
    }

    public override void InitializeSkill()
    {
        PlayerSkills.GetPassiveSkills += CreateSkills.CreateIncreasedScoresSkill;
    }
}
