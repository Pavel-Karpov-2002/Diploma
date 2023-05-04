using UnityEngine;

[CreateAssetMenu(fileName = "GiveAdditionalQuestionSkill.Asset", menuName = "Skills/GiveAdditionalQuestionSkill")]
public class GiveAdditionalQuestionSkill : Skill
{
    private int amountAdditionalQuestions;
    public override string SkillInformation => "¬ одном древнем городе жил мудрец.  ак-то раз он заскучал и решил создать артефакт, который задавал бы мудрецу сложные вопросы, которые заставл€ли его искать ответы на них по всему свету." +
        "<br><br><b>ѕредмет добавл€ет дополнительные вопросы к тесту.</b><br>";

    public override string SkillName => " нига знаний";

    public GiveAdditionalQuestionSkill(int amountAdditionalQuestions) : base()
    {
        this.amountAdditionalQuestions = amountAdditionalQuestions;
    }


    public override void Activate()
    {
        DialogScript.GetInstance().AmountQuestionPerOneNPC += amountAdditionalQuestions;
    }

    public override void InitializeSkill()
    {
        PlayerSkills.GetPassiveSkills += CreateSkills.CreateGiveAdditionalQuestionSkill;
    }
}
