using TMPro;

public class GettingResponseSkillDecorator : SkillDecorator
{
    private float countUses;
    private TextMeshProUGUI responseText;

    public GettingResponseSkillDecorator(Skill skill, float countUses, TextMeshProUGUI responseText) : base(skill)
    {
        base.skill = skill;
        this.countUses = countUses;
        this.responseText = responseText;
    }

    public override void Activate()
    {
        base.Activate();

        countUses--;

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

    protected override string SetNameInformation()
    {
        return base.SetNameInformation() + " оличество использований: " + countUses + " раз за этаж\n";
    }
}
