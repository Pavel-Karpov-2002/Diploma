public class IncreasedScoresSkillDecorator : SkillDecorator
{
    private int countPoints;

    public IncreasedScoresSkillDecorator(Skill skill, int countPoints) : base(skill)
    {
        this.countPoints = countPoints;
    }

    public override void Activate()
    {
        base.Activate();

        ((PlayerScores)PlayerConstructor.GetInstance()).NumberOfPointsPerQuestions += countPoints;
    }

    protected override string SetNameInformation()
    {
        return base.SetNameInformation() + " оличество дополнительных баллов: " + countPoints + " за вопрос";
    }
}
