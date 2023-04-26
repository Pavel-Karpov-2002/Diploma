public class AddingTimeSkillDecorator : SkillDecorator
{
    private float addingPercentTime;

    public AddingTimeSkillDecorator(Skill skill, float addingPercentTime) : base(skill)
    {
        base.skill = skill;
        this.addingPercentTime = addingPercentTime;
    }

    public override void Activate()
    {
        base.Activate();

        TimerDialogScript timer = TimerDialogScript.GetInstance();
        timer.StartTimer(timer.TimeDuration * addingPercentTime);
    }

    protected override string SetNameInformation()
    {
        return base.SetNameInformation() + "Freezing timer: " + addingPercentTime + "\n";
    }
}
