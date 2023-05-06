using UnityEngine;

[CreateAssetMenu(fileName = "AddingTimeSkill.Asset", menuName = "CustomParameters/Skills/AddingTimeSkill")]
public class AddingTimeSkill : Skill
{
    private float addingPercentTime;
    private int amountUseTimeSkill;

    public override string SkillName => "Мозг в банке";

    public override string SkillInformation => "В одном далеком королевстве жил мудрый маг, который был известен своими быстрыми ответами на любые вопросы. Однажды он решил сохранить свой мозг в банке, чтобы продлить свою жизнь и продолжать отвечать на вопросы. Но после того, как его мозг был помещен в банку, он стал отвечать на вопросы медленнее. Так что если вы хотите сохранить свой мозг в банке, будьте готовы к тому, что ответы будут приходить медленнее." +
        "<br><br><b>Предмет увеличивает время ответа на вопрос.</b>";

    public AddingTimeSkill(float addingPercentTime, int amountUseTimeSkill) : base()
    {
        this.addingPercentTime = addingPercentTime;
        this.amountUseTimeSkill = amountUseTimeSkill;
    }

    public override void Activate()
    {
        if (amountUseTimeSkill < 0)
            return;

        TimerDialogScript timer = TimerDialogScript.GetInstance();
        timer.StartTimer(timer.TimeDuration * addingPercentTime);
        amountUseTimeSkill--;
    }

    public override void InitializeSkill()
    {
        PlayerSkills.GetActiveSkills += CreateSkills.CreateTimeSkill;
    }
}
