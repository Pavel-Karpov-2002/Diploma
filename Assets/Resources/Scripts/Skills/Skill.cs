public class Skill
{
    public string NameInformation => SetNameInformation();
    public const string nameInformation = "Свойство: ";

    public virtual void Activate()
    {
        return;
    }

    protected virtual string SetNameInformation()
    {
        return nameInformation;
    } 
}
