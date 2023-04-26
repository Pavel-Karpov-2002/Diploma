public class SkillDecorator : Skill
{
    protected Skill skill;

    public SkillDecorator(Skill skill)
    {
        this.skill = skill; 
    }

    public override void Activate()
    {
        skill.Activate();
    }

    protected override string SetNameInformation()
    {
        return base.SetNameInformation() + "\n";
    }
}
