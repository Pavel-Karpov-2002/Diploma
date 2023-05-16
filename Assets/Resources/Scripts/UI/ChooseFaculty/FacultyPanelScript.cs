using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FacultyPanelScript : CustomSingleton<FacultyPanelScript>
{
    [SerializeField] private GameParameters gameParameters;
    [SerializeField] private FacultyParameters facultyParameters;
    [SerializeField] private PanelButton facultyButton;
    [SerializeField] private GameObject facultyPanel;

    public static List<LevelInformation> levelsInformation;

    private delegate void NextPanelDelegate(string path);
    private delegate void ButtonActivateFunction();
    public FacultyParameters FacultyParameters => facultyParameters;

    private void OnEnable()
    {
        CreateListOfFaculies(facultyParameters.FoldersPath);
    }

    public void CreateListOfFaculies(string path)
    {
        if (Directory.GetFiles(path, "*.json").Length > 0)
        {
            CreateButtonsLevels(path);
            return;
        }
        ChangePanelScript.CreateButtonsInPanel(Directory.EnumerateDirectories(path),
            facultyButton,
            facultyPanel,
            CreateListOfFaculies);
    }

    private void CreateButtonsLevels(string path)
    {
        ChangePanelScript.ClearPanel(facultyPanel);
        try
        {
            IEnumerable<string> pathes = Directory.GetFiles(path, "*.json");
            string completedPath = SetLevelInformation(path);

            foreach (var levelPath in pathes)
            {
                if (levelPath.Equals(completedPath))
                    continue;
                string levelName = ChangePanelScript.GetLastName(levelPath.Replace(".json", ""));
                PanelButton button = ChangePanelScript.CreateButton("Пройти этаж: " + ChangePanelScript.GetLastName(levelName),
                    facultyButton,
                    facultyPanel,
                    null,
                    () => DialogScript.GetQuestions += (path) => FileReader.ReadJsonWithTypes<NPCQuestions>(FileEncryption.ReadFile(path)),
                    () => SceneChangeScript.GetInstance().ChangeScene(gameParameters.FloorSceneName),
                    () => DialogScript.Path = levelPath);
                SetCompletedButton(button, ChangePanelScript.GetLastName(levelName));
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private string SetLevelInformation(string path)
    {
        string completedPath = "";
        try
        {
            completedPath = Directory.GetFiles(path, "completedLevels.json")[0];
            levelsInformation = FileReader.ReadJsonWithTypes<List<LevelInformation>>(FileEncryption.ReadFile(completedPath));
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            levelsInformation = new List<LevelInformation>();
        }
        return completedPath;
    }

    private void SetCompletedButton(PanelButton button, string levelName)
    {
        foreach (var levelInformation in levelsInformation)
        {
            if (levelInformation.LevelCompletedName.Equals(levelName))
            {
                button.Button.image.color = gameParameters.PassedLevelColor;
                button.ButtonText.text += "\n Рекорд: (" + levelInformation.LevelRecord + ") ";
            }
        }
    }
}
