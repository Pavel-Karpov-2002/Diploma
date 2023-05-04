using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Teacher : NPC
{
    [SerializeField] private TextMeshPro scoreToHave;
    [SerializeField] private Color colorText;

    private DialogParameters dialog;
    private int startScores;
    private int countCorrectAnswers;
    private int countResponses;
    private int countedScoreToSay;

    private void Start()
    {
        dialog = gameParameters.Dialog;
        startScores = ((PlayerScores)PlayerConstructor.GetInstance()).Scores;
        countedScoreToSay = SetCountedScoreToSay() + startScores;
        scoreToHave.text = "<color=#" + colorText.ToHexString() + ">" + countedScoreToSay.ToString() + "</color>";
    }

    private int SetCountedScoreToSay()
    {
        NPCParameters nPCParameters = gameParameters.NPC;
        int countNPCOnFloor = (gameParameters.Maze.MinNPCCountOnFloor - 1) + (GameSaveParameters.OccupiedFloor / nPCParameters.AdditionalNPCTroughtFloor);
        int countPointsPerNPC = dialog.AmountQuestionPerOneNPC * gameParameters.Player.MinPointsForCorrectAnswer;
        float percentOfCorrectAnswers = nPCParameters.PercentOfCorrectAnswers / 100;
        return (int)(countNPCOnFloor * countPointsPerNPC * percentOfCorrectAnswers);
    }

    public override void SetExpectation(bool expectation)
    {
        if (IsNormFulfilled())
        {
            IsExpectation = expectation;
            scoreToHave.text = "";
            AnswerButton.ChangeColorButton = false;
            DialogScript.FilePath = dialog.FilePathForTeachers;
            DialogScript.UrlSiteQuestion = dialog.UrlForTeachers;
            AnswerButton.OnPlayerAnswered += ChangeScoreOnCompletedDialog;
            NewQuestions(dialog.FilePathForTeachers, dialog.UrlForTeachers);
        }
    }

    private bool IsNormFulfilled()
    {
        return (startScores + ((PlayerScores)PlayerConstructor.GetInstance()).Scores) >= countedScoreToSay;
    }

    private void ChangeScoreOnCompletedDialog(bool isCorrect)
    {
        countResponses++;

        if (isCorrect)
            countCorrectAnswers++;

        int amountQuestionPerOneNPC = DialogScript.GetInstance().AmountQuestionPerOneNPC;

        if (amountQuestionPerOneNPC > countResponses)
            return;

        NPCParameters nPCParameters = gameParameters.NPC;
        float min = nPCParameters.MinPercentageOfCompletion / 100;
        int countQuestions = amountQuestionPerOneNPC;
        PlayerScores scores = ((PlayerScores)PlayerConstructor.GetInstance());

        bool isPassed = countCorrectAnswers >= (int)(countQuestions * min);

        scores.ChangeScores(isPassed ? countQuestions * scores.NumberOfPointsForCorrectAnswer : (countQuestions * scores.NumberOfPointsForWrongAnswer * -1));

        DialogScript.GetInstance().CloseDialogWindow();

        if (isPassed && scores.Scores > 0)
        {
            SceneChangeScript.GetInstance().NextFloor();
            return;
        }

        SceneChangeScript.GetInstance().LoseScene();
    }
}
