using TMPro;
using UnityEngine;

public class PlayerSkills : PlayerConstructor
{
    [SerializeField] private GameObject skillButton;
    [SerializeField] private GameObject skillsPanel;
    [SerializeField] private TextMeshProUGUI response;
    [SerializeField] private SkillsParameters skillsParameters;

    public delegate Skill GetPassiveSkillsDelegate(SkillsParameters skillsParameters);
    public delegate GameObject GetActiveSkillsDelegate(GameObject skillButton, GameObject skillsPanel, SkillsParameters skillsParameters);
    public delegate GameObject GetResponseSkillDelegate(GameObject skillButton, GameObject skillsPanel, SkillsParameters skillsParameters, params object[] obj);

    public static event GetActiveSkillsDelegate GetActiveSkills;
    public static event GetPassiveSkillsDelegate GetPassiveSkills;
    public static event GetResponseSkillDelegate GetResponseSkill;

    private void Start()
    {
        foreach (var item in GameData.Data.PlayerItems)
            item.Skill.InitializeSkill();
        GetPassiveSkills?.Invoke(skillsParameters);
        GetActiveSkills?.Invoke(skillButton, skillsPanel, skillsParameters);
        GetResponseSkill?.Invoke(skillButton, skillsPanel, skillsParameters, response);
    }
}
