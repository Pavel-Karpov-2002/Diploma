using UnityEngine;

[CreateAssetMenu(fileName = "IncreasedScoresSkill.Asset", menuName = "Skills/IncreasedScoresSkill")]
public class IncreasedScoresSkill : Skill
{
    private int amountPoints;

    public override string SkillInformation => "¬ древние времена жил ученый, который использовал перо, которое помогало ему лучше осмысл€ть свои ответы, расписыва€ их на бумаге." +
        "<br><br><b>ѕредмет дает дополнительные баллы за верные ответы на вопросы.</b><br>";

    public override string SkillName => "ѕеро знаний";

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
