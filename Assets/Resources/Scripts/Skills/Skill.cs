public class Skill
{
    public string NameInformation => SetNameInformation();
    public const string nameInformation = "��������: ";

    public virtual void Activate()
    {
        return;
    }

    protected virtual string SetNameInformation()
    {
        return nameInformation;
    } 
}
