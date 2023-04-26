public class SkipQuestionSkillDecorator : SkillDecorator
{
    private int countSkipQuestions;

    public int CountSkipQuestions => countSkipQuestions;

    public SkipQuestionSkillDecorator(Skill skill, int countSkipQuestions) : base(skill)
    {
        this.countSkipQuestions = countSkipQuestions;
    }

    public override void Activate()
    {
        base.Activate();

        DialogScript.GetInstance().ShowNewQuestion();
        countSkipQuestions--;
    }

    protected override string SetNameInformation()
    {
        return base.SetNameInformation() + " оличество использований: " + countSkipQuestions + " раз за этаж\n";
    }
}
