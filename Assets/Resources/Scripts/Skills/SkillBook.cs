using TMPro;
using UnityEngine;

public class SkillBook : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI response;

    public void AddingTimeSkill()
    {
        SkillDecorator skill = new AddingTimeSkillDecorator(new Skill(), 10);
        skill.Activate();
    }

    public void IncreasedScoresSkill()
    {
        SkillDecorator skill = new IncreasedScoresSkillDecorator(new Skill(), 10);
        skill.Activate();
    }

    public void GettingResponseSkill()
    {
        SkillDecorator skill = new GettingResponseSkillDecorator(new Skill(), 10, response);
        skill.Activate();
    }

    public void GiveAdditionalQuestionSkill()
    {
        SkillDecorator skill = new GiveAdditionalQuestionSkillDecorator(new Skill(), 3);
        skill.Activate();
    }

    public void SkipQuestionSkill()
    {
        SkillDecorator skill = new SkipQuestionSkillDecorator(new Skill(), 3);
        skill.Activate();
    }
}
