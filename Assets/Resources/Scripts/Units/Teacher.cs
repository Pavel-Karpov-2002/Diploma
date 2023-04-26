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
        countedScoreToSay = SetCountedScoreToSay();
        scoreToHave.text = "<color=#" + colorText.ToHexString() + ">" + countedScoreToSay.ToString() + "</color>";
    }

    private int SetCountedScoreToSay()
    {
        NPCParameters nPCParameters = gameParameters.NPC;
        int countNPCOnFloor = (gameParameters.Maze.MinNPCCountOnFloor - 1) + (FloorInformation.OccupiedFloor / nPCParameters.AdditionalNPCTroughtFloor);
        int countPointsPerNPC = dialog.AmountQuestionPerOneNPC * gameParameters.Player.MinPointsForQuestion;
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

        if (DialogScript.GetInstance().AmountQuestionPerOneNPC > countResponses)
            return;

        NPCParameters nPCParameters = gameParameters.NPC;
        float min = nPCParameters.MinPercentageOfCompletion / 100;
        int countPointsPerNPC = DialogScript.GetInstance().AmountQuestionPerOneNPC * gameParameters.Player.MinPointsForQuestion;

        ((PlayerScores)PlayerConstructor.GetInstance()).ChangeScores(countCorrectAnswers >= (int)(DialogScript.GetInstance().AmountQuestionPerOneNPC * min) ? countPointsPerNPC : -countPointsPerNPC);
    }
}
