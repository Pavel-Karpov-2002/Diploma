using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Teacher : NPC
{
    [SerializeField] private TextMeshPro scoreToHave;
    [SerializeField] private Color colorText;

    private int countCorrectAnswers;
    private int amountResponses;
    private int countedScoreToSay;
    private int amountPointsCorrectAnswers;
    private int amountPointsForWrongAnswer;

    private void Start()
    {
        countedScoreToSay = SetCountedScoreToSay();
        amountResponses = DialogPanelSingleton.GetInstance().NpcQuestions.Teacher.AmountQuestionsForTest;
        scoreToHave.text = "<color=#" + colorText.ToHexString() + ">" + countedScoreToSay + "</color>";
    }

    private int SetCountedScoreToSay()
    {
        NPCQuestions npcParameters = DialogPanelSingleton.GetInstance().NpcQuestions;
        int countPoints = 0;
        int j = 0;
        for (int i = 0; i < npcParameters.Student.AmountQuestionsForTest * npcParameters.AmountStudentsOnFloor; i++)
        {
            if (j >= npcParameters.Student.Questions.Length)
                j = 0;
            countPoints += npcParameters.Student.Questions[j].PointsForCorrectAnswer;
            j++;
        }
        float percent = (npcParameters.MinPercentCorrectToUnlockTeacher / 100f);
        int result = (int)(countPoints * percent);
        return result;
    }

    public override void SetExpectation(bool expectation)
    {
        if (IsNormFulfilled())
        {
            IsExpectation = expectation;
            scoreToHave.text = "";
            AnswerButton.ChangeColorButton = false;
            AnswerButton.OnPlayerAnswered += ChangeScoreOnCompletedDialog;
            DialogScript.NpcType = NPCType.Teacher;
            NewQuestions();
        }
    }

    private bool IsNormFulfilled()
    {
        return PlayerScores.GetInstance().Scores >= countedScoreToSay;
    }

    private void ChangeScoreOnCompletedDialog(int amountPoints)
    {
        amountResponses--;
        if (amountPoints > 0)
        {
            amountPointsCorrectAnswers += amountPoints;
            countCorrectAnswers++;
        }
        else
        {
            amountPointsForWrongAnswer += amountPoints;
        }
        if (amountResponses > 0)
            return;

        NPCQuestions npcParameters = DialogPanelSingleton.GetInstance().NpcQuestions;
        float percent = (npcParameters.MinPercentCorrectTeacher / 100f);
        bool isPassed = countCorrectAnswers >= (npcParameters.Teacher.AmountQuestionsForTest * percent);
        PlayerScores.GetInstance().ChangeScores(isPassed ? amountPointsCorrectAnswers : amountPointsForWrongAnswer);
        SceneChangeScript.GetInstance().MainMenuScene();
    }
}
