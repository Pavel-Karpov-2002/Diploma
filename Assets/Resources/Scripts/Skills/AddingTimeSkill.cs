using UnityEngine;

[CreateAssetMenu(fileName = "AddingTimeSkill.Asset", menuName = "CustomParameters/Skills/AddingTimeSkill")]
public class AddingTimeSkill : Skill
{
    private float addingPercentTime;
    private int amountUses;

    public int AmountUses => amountUses;

    public AddingTimeSkill(float addingPercentTime, int amountUses) : base()
    {
        this.addingPercentTime = (addingPercentTime / 100f);
        this.amountUses = amountUses;
    }

    public override void Activate()
    {
        if (amountUses < 0)
            return;
        TimerDialogScript timer = TimerDialogScript.Instance;
        timer.StartTimer(timer.TimeRemaining + timer.TimeDuration * addingPercentTime);
        amountUses--;
    }

    public override void InitializeSkill()
    {
        PlayerSkills.GetActiveSkills += CreateSkills.CreateTimeSkill;
    }
}
