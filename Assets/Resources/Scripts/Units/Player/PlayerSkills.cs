using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerSkills : Singleton<PlayerSkills>
{
    [SerializeField] private GameObject skillButton;
    [SerializeField] private GameObject skillsPanel;
    [SerializeField] private TextMeshProUGUI responseText;
    [SerializeField] private SkillsParameters skillsParameters;
    [SerializeField] private GameParameters gameParameters;
    [SerializeField] private AudioParameters audioParameters;

    public delegate Skill GetPassiveSkillsDelegate(SkillsParameters skillsParameters);
    public delegate SkillButton GetActiveSkillsDelegate(GameObject skillButton, GameObject skillsPanel, SkillsParameters skillsParameters, GameParameters gameParameters);
    public delegate SkillButton GetResponseSkillDelegate(GameObject skillButton, GameObject skillsPanel, SkillsParameters skillsParameters, GameParameters gameParameters, params object[] obj);

    public static event GetActiveSkillsDelegate GetActiveSkills;
    public static event GetPassiveSkillsDelegate GetPassiveSkills;
    public static event GetResponseSkillDelegate GetResponseSkill;

    private List<SkillButton> buttons;
    public AudioParameters AudioParameters => audioParameters;

    public GameObject SkillsPanel => skillsPanel;

    protected override void Awake()
    {
        base.Awake();
        buttons = new List<SkillButton>();
    }

    private void Start()
    {
        CreateSkillsButtons();
    }

    private void CreateSkillsButtons()
    {
        ChangePanelScript.ClearPanel(skillsPanel);
        foreach (var item in GameData.Data.PlayerItems)
            item.Skill.InitializeSkill();
        GetPassiveSkills?.Invoke(skillsParameters);

        if (GetActiveSkills != null)
            buttons = GetActiveSkills
                .GetInvocationList()
                .Cast<GetActiveSkillsDelegate>()
                .Select(x => x?.Invoke(skillButton, skillsPanel, skillsParameters, gameParameters))
                .ToList();
        if (GetResponseSkill != null)
        {
            foreach (var response in GetResponseSkill.GetInvocationList().Cast<GetResponseSkillDelegate>().Select(x => x?.Invoke(skillButton, skillsPanel, skillsParameters, gameParameters, responseText)).ToList())
            {
                buttons.Add(response);
            }
        }
    }

    public void SetInteractableSkillsButton()
    {
        responseText.text = string.Empty;
        foreach (var button in buttons)
        {
            if (button.AmountUses > 0)
            {
                button.Button.interactable = true;
            }
        }
    }

    private void OnDestroy()
    {
        GetPassiveSkills = null;
        GetActiveSkills = null;
        GetResponseSkill = null;
    }
}
