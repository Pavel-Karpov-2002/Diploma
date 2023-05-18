using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateSkills : MonoBehaviour
{
    private delegate void SetListenerDelegate();

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

    public static SkillButton CreateSkipQuestionSkill(GameObject skillButton, GameObject skillsPanel, SkillsParameters skillsParameters, GameParameters gameParameters)
    {
        SkipQuestionSkill skill = new SkipQuestionSkill(skillsParameters.AmountSkipQuestion);
        SkillButton button = CreateButton(skillButton, skillsPanel).GetComponent<SkillButton>();
        SetListeners(button, skill, gameParameters.Items);
        button.TextMeshPro.text = skill.AmountUses.ToString();
        button.AmountUses = skill.AmountUses;
        button.Button.onClick.AddListener(() => button.TextMeshPro.text = button.AmountUses.ToString());
        return button;
    }

    public static SkillButton CreateTimeSkill(GameObject skillButton, GameObject skillsPanel, SkillsParameters skillsParameters, GameParameters gameParameters)
    {
        AddingTimeSkill skill = new AddingTimeSkill(skillsParameters.AddedPercentTimeCountSkill, skillsParameters.AmountUseTimeSkill);
        SkillButton button = CreateButton(skillButton, skillsPanel).GetComponent<SkillButton>();
        SetListeners(button, skill, gameParameters.Items);
        button.TextMeshPro.text = skill.AmountUses.ToString();
        button.AmountUses = skill.AmountUses;
        button.Button.onClick.AddListener(() => button.TextMeshPro.text = button.AmountUses.ToString());
        return button;
    }

    public static SkillButton CreateGettingResponseSkill(GameObject skillButton, GameObject skillsPanel, SkillsParameters skillsParameters, GameParameters gameParameters, params object[] obj)
    {
        GettingResponseSkill skill = new GettingResponseSkill(skillsParameters.AmountUsesGetResponse, (TextMeshProUGUI)obj[0]);
        SkillButton button = CreateButton(skillButton, skillsPanel).GetComponent<SkillButton>();
        SetListeners(button, skill, gameParameters.Items);
        button.TextMeshPro.text = skill.AmountUses.ToString();
        button.AmountUses = skill.AmountUses;
        button.Button.onClick.AddListener(() => button.TextMeshPro.text = skill.AmountUses.ToString());
        return button;
    }

    private static GameObject CreateButton(GameObject skillButton, GameObject skillsPanel)
    {
        GameObject button = Instantiate(skillButton);
        button.transform.SetParent(skillsPanel.transform);
        button.transform.localScale = skillButton.transform.localScale;
        button.transform.position = skillButton.transform.position;
        Vector3 pos = button.transform.position;
        pos.z = 0;
        button.transform.position = pos;
        return button;
    }

    private static void SetListeners(SkillButton button, Skill skill, List<Item> items)
    {
        button.Button.onClick.AddListener(() => button.Button.interactable = false);
        button.Button.onClick.AddListener(() => button.AmountUses--);
        button.Button.onClick.AddListener(() => skill.Activate());
        button.Button.onClick.AddListener(() => AudioController.Instance.PlayOneAudio(PlayerSkills.Instance.AudioParameters.ItemUsed));
        foreach (var item in items)
        {
            if (item.Skill.GetType() == skill.GetType())
            {
#if UNITY_EDITOR
                string path = Application.streamingAssetsPath;
#elif UNITY_ANDROID             
                string path = Application.persistentDataPath;
#endif
                button.Image.sprite = Resources.Load<Sprite>(item.ItemSpritePath);
                break;
            }
        }
    }
}
