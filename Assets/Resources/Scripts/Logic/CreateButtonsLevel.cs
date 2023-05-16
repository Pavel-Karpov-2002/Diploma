using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreateButtonsLevel
{
    public static List<LevelInformation> levelsInformation = new List<LevelInformation>();

    public static void CreateButtons(string path, GameObject panel, PanelButton button, AudioClip nextSceneAudio, GameParameters gameParameters)
    {
        ChangePanelScript.ClearPanel(panel);
        levelsInformation = new List<LevelInformation>();
        try
        {
            IEnumerable<string> pathes = Directory.GetFiles(path, "*.json");
            string completedPath = SetLevelInformation(path);

            foreach (var levelPath in pathes)
            {
                if (levelPath.Equals(completedPath))
                    continue;
                string levelName = ChangePanelScript.GetLastName(levelPath.Replace(".json", ""));
                PanelButton panelButton = ChangePanelScript.CreateButton("Пройти этаж: " + ChangePanelScript.GetLastName(levelName),
                    button,
                    panel,
                    null,
                    () => AudioController.Instance.PlayOneAudio(nextSceneAudio),
                    () => DialogScript.GetQuestions += (path) => FileOperations.ReadJsonWithTypes<NPCQuestions>(FileEncryption.ReadFile(path)),
                    () => SceneChangeScript.GetInstance().ChangeScene(gameParameters.FloorSceneName),
                    () => DialogScript.Path = levelPath);
                SetCompletedButton(panelButton, ChangePanelScript.GetLastName(levelName), gameParameters.PassedLevelColor);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    private static string SetLevelInformation(string path)
    {
        string completedPath = "";
        try
        {
            completedPath = Directory.GetFiles(path, "completedLevels.json")[0];
            levelsInformation = FileOperations.ReadJsonWithTypes<List<LevelInformation>>(FileEncryption.ReadFile(completedPath));
        }
        catch
        {
            levelsInformation = new List<LevelInformation>();
        }
        return completedPath;
    }

    private static void SetCompletedButton(PanelButton button, string levelName, Color buttonColor)
    {
        foreach (var levelInformation in levelsInformation)
        {
            if (levelInformation.LevelCompletedName.Equals(levelName))
            {
                button.Image.color = buttonColor;
                button.ButtonText.text += "\n Рекорд: (" + levelInformation.LevelRecord + ") ";
            }
        }
    }

}
