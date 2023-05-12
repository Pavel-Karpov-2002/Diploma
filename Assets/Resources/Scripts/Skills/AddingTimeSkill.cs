using UnityEngine;

[CreateAssetMenu(fileName = "AddingTimeSkill.Asset", menuName = "CustomParameters/Skills/AddingTimeSkill")]
public class AddingTimeSkill : Skill
{
    private float addingPercentTime;
    private int amountUseTimeSkill;

    public AddingTimeSkill(float addingPercentTime, int amountUseTimeSkill) : base()
    {
        this.addingPercentTime = (addingPercentTime / 100f);
        this.amountUseTimeSkill = amountUseTimeSkill;
    }

    public override void Activate()
    {
        if (amountUseTimeSkill < 0)
            return;
        TimerDialogScript timer = TimerDialogScript.Instance;
        timer.StartTimer(timer.TimeRemaining + timer.TimeDuration * addingPercentTime);
        amountUseTimeSkill--;
    }

    public override void InitializeSkill()
    {
        PlayerSkills.GetActiveSkills += CreateSkills.CreateTimeSkill;
    }
}
