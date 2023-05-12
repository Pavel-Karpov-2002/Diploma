using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CreateSkills : MonoBehaviour
{
    public static IncreaseSpeedSkill CreateIncreaseSpeedSkill(SkillsParameters skillsParameters)
    {
        IncreaseSpeedSkill skill = new IncreaseSpeedSkill(skillsParameters.AmountAdditionPercentagelSpeed);
        skill.Activate();
        return skill;
    }

    public static Skill CreateIncreasedScoresSkill(SkillsParameters skillsParameters)
    {
        IncreasedScoresSkill skill = new IncreasedScoresSkill(skillsParameters.AmountIncreasedPointsInPercentage);
        skill.Activate();
        return skill;
    }

    public static Button CreateSkipQuestionSkill(GameObject skillButton, GameObject skillsPanel, SkillsParameters skillsParameters, GameParameters gameParameters)
    {
        Button button = CreateButton(skillButton, skillsPanel).GetComponent<Button>();
        SkipQuestionSkill skill = new SkipQuestionSkill(skillsParameters.AmountSkipQuestion);
        button.onClick.AddListener(() => skill.Activate());
        button.onClick.AddListener(() => button.interactable = false);
        foreach (var item in gameParameters.Items)
        {
            if (item.Skill is SkipQuestionSkill)
            {
                button.image.sprite = ConvertTexture2D.GetSprite(ConvertTexture2D.GetTexture2D(item.ItemSpritePath));
                break;
            }
        }
        return button;
    }

    public static Button CreateTimeSkill(GameObject skillButton, GameObject skillsPanel, SkillsParameters skillsParameters, GameParameters gameParameters)
    {
        Button button = CreateButton(skillButton, skillsPanel).GetComponent<Button>();
        AddingTimeSkill timeSkill = new AddingTimeSkill(skillsParameters.AddedPercentTimeCountSkill, skillsParameters.AmountUseTimeSkill);
        button.onClick.AddListener(() => timeSkill.Activate());
        button.onClick.AddListener(() => button.interactable = false);
        foreach (var item in gameParameters.Items)
        {
            if (item.Skill is AddingTimeSkill)
            {
                button.image.sprite = ConvertTexture2D.GetSprite(ConvertTexture2D.GetTexture2D(item.ItemSpritePath));
                break;
            }
        }
        return button;
    }

    public static Button CreateGettingResponseSkill(GameObject skillButton, GameObject skillsPanel, SkillsParameters skillsParameters, GameParameters gameParameters, params object[] obj)
    {
        Button button = CreateButton(skillButton, skillsPanel).GetComponent<Button>();
        GettingResponseSkill timeSkill = new GettingResponseSkill(skillsParameters.AmountUsesGetResponse, (TextMeshProUGUI)obj[0]);
        button.onClick.AddListener(() => timeSkill.Activate());
        button.onClick.AddListener(() => button.interactable = false);
        foreach (var item in gameParameters.Items)
        {
            if (item.Skill is GettingResponseSkill)
            {
                button.image.sprite = ConvertTexture2D.GetSprite(ConvertTexture2D.GetTexture2D(item.ItemSpritePath));
                break;
            }
        }
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
