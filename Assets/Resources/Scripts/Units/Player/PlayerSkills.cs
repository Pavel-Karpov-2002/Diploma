using TMPro;
using UnityEngine;

public class PlayerSkills : PlayerConstructor
{
    [SerializeField] private GameObject skillButton;
    [SerializeField] private GameObject skillsPanel;
    [SerializeField] private TextMeshProUGUI response;

    public delegate Skill GetPassiveSkillsDelegate(SkillsParameters skillsParameters);
    public delegate GameObject GetActiveSkillsDelegate(GameObject skillButton, GameObject skillsPanel, SkillsParameters skillsParameters);
    public delegate GameObject GetResponseSkillDelegate(GameObject skillButton, GameObject skillsPanel, SkillsParameters skillsParameters, params object[] obj);

    public static event GetActiveSkillsDelegate GetActiveSkills;
    public static event GetPassiveSkillsDelegate GetPassiveSkills;
    public static event GetResponseSkillDelegate GetResponseSkill;

    private void Start()
    {
        SkillsParameters skillsParameters = GameParameters.Skills;

        GetPassiveSkills?.Invoke(skillsParameters);
        GetActiveSkills?.Invoke(skillButton, skillsPanel, skillsParameters);
        GetResponseSkill?.Invoke(skillButton, skillsPanel, skillsParameters, response);
    }
}
