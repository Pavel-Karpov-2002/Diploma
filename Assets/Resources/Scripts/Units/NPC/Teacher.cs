using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Teacher : NPC
{
    [SerializeField] private GameParameters gameParameters;
    [SerializeField] private AudioParameters audioParameters;
    [SerializeField] private TextMeshPro scoreToHave;
    [SerializeField] private Color colorText;

    private int countCorrectAnswers;
    private int amountResponses;
    private int countedScoreToSay;
    private int amountPointsCorrectAnswers;

    private void Start()
    {
        SetSkin();
        countedScoreToSay = SetCountedScoreToSay();
        amountResponses = DialogScript.Instance.NpcQuestions.Teacher.AmountQuestionsForTest;
        scoreToHave.text = "<color=#" + colorText.ToHexString() + ">" + countedScoreToSay + "</color>";
        SetSkin();
    }

    private int SetCountedScoreToSay()
    {
        NPCQuestions npcParameters = DialogScript.Instance.NpcQuestions;
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
            DialogScript.Instance.ShowNewQuestion();
        }
        else if (DialogScript.countNPCCompleted >= DialogScript.Instance.NpcQuestions.AmountStudentsOnFloor - 1)
        {
            SceneChangeScript.GetInstance().ChangeScene(gameParameters.LobbySceneName);
        }
    }

    private bool IsNormFulfilled()
    {
        return PlayerScores.Instance.Scores >= countedScoreToSay;
    }

    private void ChangeScoreOnCompletedDialog(int amountPoints)
    {
        amountResponses--;
        if (amountPoints > 0)
        {
            amountPointsCorrectAnswers += amountPoints;
            countCorrectAnswers++;
        }

        if (amountResponses > 0)
            return;

        NPCQuestions npcParameters = DialogScript.Instance.NpcQuestions;
        float percent = (npcParameters.MinPercentCorrectTeacher / 100f);
        bool isPassed = countCorrectAnswers >= (npcParameters.Teacher.AmountQuestionsForTest * percent);
        if (isPassed)
        {
            GameData.Data.AmountMoney += amountPointsCorrectAnswers;
            FloorCompleted(PlayerScores.Instance.Scores + amountPointsCorrectAnswers);
#if UNITY_EDITOR
            string path = Application.streamingAssetsPath;
#elif UNITY_ANDROID             
            string path = Application.persistentDataPath;
#endif
            GameData.UpdateGameDataFile(path + gameParameters.DataPath);
        }

        AudioController.Instance.PlayOneAudio(isPassed ? audioParameters.LevelPassed : audioParameters.LevelNotPassed);
        DialogScript.Instance.CloseDialogWindow();
        SceneChangeScript.GetInstance().ChangeScene(gameParameters.LobbySceneName);
    }

    private void FloorCompleted(int scorePoints)
    {
        if (CreateButtonsLevel.levelsInformation == null)
            CreateButtonsLevel.levelsInformation = new List<LevelInformation>();

        foreach (var levelInformation in CreateButtonsLevel.levelsInformation)
        {
            if (levelInformation.LevelCompletedName.Equals(System.IO.Path.GetFileNameWithoutExtension(DialogScript.Path)))
            {
                if (levelInformation.LevelRecord < scorePoints)
                {
                    levelInformation.LevelRecord = scorePoints;
                    SaveCompletedLevel();
                }
                return;
            }
        }
        CreateButtonsLevel.levelsInformation.Add(new LevelInformation() { LevelCompletedName = System.IO.Path.GetFileNameWithoutExtension(DialogScript.Path), LevelRecord = scorePoints });
        GameData.Data.AmountPassedLevels += 1;
        SaveCompletedLevel();
    }

    private void SaveCompletedLevel()
    {
#if UNITY_EDITOR
        string path = Application.streamingAssetsPath;
#elif UNITY_ANDROID             
        string path = Application.persistentDataPath;
#endif
        FileEncryption.WriteFile(path + "/" + System.IO.Path.GetDirectoryName(DialogScript.Path) + "/completedLevels.json", CreateButtonsLevel.levelsInformation);
    }

    protected override void SetSkin()
    {
        npcSkin.sprite = npcParameters.Teachers[Random.Range(0, npcParameters.Teachers.Count - 1)].Skin;
        npcAnimator.runtimeAnimatorController = npcParameters.Teachers[Random.Range(0, npcParameters.Teachers.Count - 1)].SkinAnimator;
    }

    private void OnDestroy()
    {
        AnswerButton.OnPlayerAnswered -= ChangeScoreOnCompletedDialog;
    }
}
