using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class CreateButtonsLevel
{
    public static List<LevelInformation> levelsInformation = new List<LevelInformation>();

    public static void CreateButtons(string[] paths, GameObject panel, PanelButton button, AudioClip nextSceneAudio, GameParameters gameParameters, bool isLink = false)
    {
        ChangePanelScript.ClearPanel(panel);
        levelsInformation = new List<LevelInformation>();
        levelsInformation.Add(new LevelInformation());
        try
        {
            foreach (var levelPath in paths)
            {
                if (levelPath.IndexOf("completedLevels.json") != -1)
                {
#if UNITY_EDITOR
                    SetLevelInformation(Application.streamingAssetsPath + "/" + levelPath);
#elif UNITY_ANDROID
                    SetLevelInformation(Application.persistentDataPath + "/" + levelPath);
#endif
                    continue;
                }
                string levelName = ChangePanelScript.GetLastName(levelPath.Replace(".json", ""));
                NPCQuestions questions = GetNpcQuestions(levelPath, isLink);
                if (questions != null)
                {
                    PanelButton panelButton = new PanelButton();
                    panelButton = ChangePanelScript.CreateButton("Пройти этаж: " + ChangePanelScript.GetLastName(levelName),
                        button,
                        panel,
                        null,
                        () => AudioController.Instance.PlayOneAudio(nextSceneAudio),
                        () => DialogScript.GetQuestions += () => questions,
                        () => SceneChangeScript.GetInstance().ChangeScene(gameParameters.FloorSceneName),
                        () => DialogScript.Path = levelPath);
                    SetCompletedButton(panelButton, ChangePanelScript.GetLastName(levelName), gameParameters.PassedLevelColor);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    private static void SetLevelInformation(string path)
    {
        try
        {
            FileStream file = new FileStream(path, FileMode.OpenOrCreate);
            levelsInformation = FileOperations.ReadJsonWithTypes<List<LevelInformation>>(FileEncryption.ReadBytes(FileOperations.GetBytesInStream(file)));
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private static NPCQuestions GetNpcQuestions(string path, bool isLink)
    {
        if (isLink)
        {
            using (var stream = File.Open(path, FileMode.OpenOrCreate))
            {
                return FileOperations.ReadJsonWithTypes<NPCQuestions>(FileEncryption.ReadBytes(FileOperations.GetBytesInStream(stream)));
            }
        }
        return FileOperations.ReadJsonWithTypes<NPCQuestions>(FileEncryption.ReadBytes(BetterStreamingAssets.ReadFromBytes(path)));
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
