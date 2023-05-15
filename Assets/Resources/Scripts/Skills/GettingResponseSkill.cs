using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "GettingResponseSkill.Asset", menuName = "CustomParameters/Skills/GettingResponseSkill")]
public class GettingResponseSkill : Skill
{
    private int amountUses;
    private TextMeshProUGUI responseText;

    public int AmountUses => amountUses;

    public GettingResponseSkill(int amountUses, TextMeshProUGUI responseText) : base()
    {
        this.amountUses = amountUses;
        this.responseText = responseText;
    }

    public override void Activate()
    {
        if (responseText == null || amountUses <= 0)
            return;
        amountUses--;
        TestingAnswersScript buttonsAnswersTest = DialogScript.Instance.Testing;
        if (buttonsAnswersTest.gameObject.activeSelf)
        {
            foreach (var button in buttonsAnswersTest.ButtonsPanel.GetComponentsInChildren<AnswerButton>())
            {
                if (button.Answer != string.Empty)
                {
                    responseText.text = button.Answer;
                    return;
                }
            }
        }
        EnteringResponseScript buttonEnteringAnswer = DialogScript.Instance.EnteringResponse;

        if (buttonEnteringAnswer.gameObject.activeSelf)
        {
            responseText.text = buttonEnteringAnswer.AnswerButton.Answer;
            return;
        }
    }

    public override void InitializeSkill()
    {
        PlayerSkills.GetResponseSkill += CreateSkills.CreateGettingResponseSkill;
    }
}
