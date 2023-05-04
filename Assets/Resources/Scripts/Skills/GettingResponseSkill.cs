using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "GettingResponseSkill.Asset", menuName = "Skills/GettingResponseSkill")]
public class GettingResponseSkill : Skill
{
    private int amountUses;
    private TextMeshProUGUI responseText;

    public override string SkillInformation => "¬ одном древнем храме сто€ло зеркало мудрости. Ћюбой человек, который смотрел на зеркало и задавал ему вопрос, получал ответ. Ќо однажды один человек задал зеркалу очень сложный вопрос. «еркало не смогло дать ему ответ и разбилось на кусочки. » из этих кусочков создали зеркала разослав по всему континенту." +
        "<br><br><b>ѕредмет демонстрирует ответ на вопрос.</b><br>";

    public override string SkillName => "«еркало мудрости";

    public GettingResponseSkill(int amountUses, TextMeshProUGUI responseText) : base()
    {
        this.amountUses = amountUses;
        this.responseText = responseText;
    }

    public override void Activate()
    {
        if (responseText == null)
            return;

        amountUses--;

        TestingAnswersScript buttonsAnswersTest = TestingAnswersScript.GetInstance();

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

        EnteringResponseScript buttonEnteringAnswer = EnteringResponseScript.GetInstance();

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
