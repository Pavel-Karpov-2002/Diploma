using UnityEngine;

[CreateAssetMenu(fileName = "IncreaseSpeedSkill.Asset", menuName = "CustomParameters/Skills/IncreaseSpeedSkill")]
public class IncreaseSpeedSkill : Skill
{
    private int addingPercentageSpeed;

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
