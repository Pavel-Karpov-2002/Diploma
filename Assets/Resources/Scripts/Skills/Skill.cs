using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public abstract string SkillInformation { get; }
    public abstract string SkillName { get; }

    public abstract void Activate();
    public abstract void InitializeSkill();
}
