using UnityEngine;

[CreateAssetMenu(fileName = "IncreasedScoresSkill.Asset", menuName = "CustomParameters/Skills/IncreasedScoresSkill")]
public class IncreasedScoresSkill : Skill
{
    private float amountPoints;

    public override string SkillInformation => "¬ древние времена жил ученый, который использовал перо, которое помогало ему лучше осмысл€ть свои ответы, расписыва€ их на бумаге." +
        "<br><br><b>ѕредмет дает дополнительные баллы за верные ответы на вопросы.</b><br>";

    public override string SkillName => "ѕеро знаний";

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
