public class GiveAdditionalQuestionSkillDecorator : SkillDecorator
{
    private int countAdditionalQuestions;

    public GiveAdditionalQuestionSkillDecorator(Skill skill, int countAdditionalQuestions) : base(skill)
    {
        this.countAdditionalQuestions = countAdditionalQuestions;
    }

    public override void Activate()
    {
        base.Activate();

        DialogScript.GetInstance().AmountQuestionPerOneNPC += countAdditionalQuestions;
    }

    protected override string SetNameInformation()
    {
        return base.SetNameInformation() + " оличество дополнительных вопросов: " + countAdditionalQuestions;
    }
}
