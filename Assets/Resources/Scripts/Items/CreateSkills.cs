using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateSkills : MonoBehaviour
{
    public static Skill CreateIncreaseSpeedSkill(SkillsParameters skillsParameters)
    {
        IncreaseSpeedSkill skill = new IncreaseSpeedSkill(skillsParameters.AmountAdditionPercentagelSpeed);
        skill.Activate();
        Debug.Log(skill.GetType() + " activate");

        return skill;
    }

    public static Skill CreateIncreasedScoresSkill(SkillsParameters skillsParameters)
    {
        IncreasedScoresSkill skill = new IncreasedScoresSkill(skillsParameters.AmountIncreasedPointsInPercentage);
        skill.Activate();

        return skill;
    }

    public static Skill CreateSkipQuestionSkill(SkillsParameters skillsParameters)
    {
        SkipQuestionSkill skill = new SkipQuestionSkill(skillsParameters.AmountSkipQuestion);
        skill.Activate();

        return skill;
    }

    public static GameObject CreateTimeSkill(GameObject skillButton, GameObject skillsPanel, SkillsParameters skillsParameters)
    {
        GameObject button = CreateButton(skillButton, skillsPanel);
        AddingTimeSkill timeSkill = new AddingTimeSkill(skillsParameters.AddedPercentTimeCountSkill, skillsParameters.AmountUseTimeSkill);
        button.GetComponent<Button>().onClick.AddListener(() => timeSkill.Activate());

        return button;
    }

    public static GameObject CreateGettingResponseSkill(GameObject skillButton, GameObject skillsPanel, SkillsParameters skillsParameters, params object[] obj)
    {
        GameObject button = CreateButton(skillButton, skillsPanel);
        GettingResponseSkill timeSkill = new GettingResponseSkill(skillsParameters.AmountUsesGetResponse, (TextMeshProUGUI)obj[0]);
        button.GetComponent<Button>().onClick.AddListener(() => timeSkill.Activate());

        return button;
    }

    private static GameObject CreateButton(GameObject skillButton, GameObject skillsPanel)
    {
        GameObject button = Instantiate(skillButton);
        button.transform.SetParent(skillsPanel.transform);
        button.transform.localScale = skillButton.transform.localScale;
        button.transform.position = skillButton.transform.position;

        return button;
    }
}
