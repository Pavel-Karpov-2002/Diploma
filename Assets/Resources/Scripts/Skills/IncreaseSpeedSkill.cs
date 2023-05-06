using UnityEngine;

[CreateAssetMenu(fileName = "IncreaseSpeedSkill.Asset", menuName = "CustomParameters/Skills/IncreaseSpeedSkill")]
public class IncreaseSpeedSkill : Skill
{
    private int addingPercentageSpeed;
    public override string SkillInformation => "¬ одном древнем городе жил мудрец.  ак-то раз он заскучал и решил создать артефакт, который задавал бы мудрецу сложные вопросы, которые заставл€ли его искать ответы на них по всему свету." +
        "<br><br><b>ѕредмет уведичивает скорость передвижени€.</b><br>";

    public override string SkillName => "—апоги-скороходы";

    public IncreaseSpeedSkill(int addingPercentageSpeed) : base()
    {
        this.addingPercentageSpeed = addingPercentageSpeed;
    }


    public override void Activate()
    {
        PlayerMovement.GetInstance().Speed += PlayerMovement.GetInstance().Speed * (addingPercentageSpeed / 100f);
    }

    public override void InitializeSkill()
    {
        PlayerSkills.GetPassiveSkills += CreateSkills.CreateIncreaseSpeedSkill;
    }
}
