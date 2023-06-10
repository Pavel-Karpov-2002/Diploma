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
#if UNITY_ANDROID       
        string comletedLevelsPath = Application.persistentDataPath + "/" + Path.GetDirectoryName(paths[0]) + "/completedLevels.json";
        if (paths.Length > 0 && File.Exists(comletedLevelsPath))
        {
            SetLevelInformation(comletedLevelsPath);
        }
#endif
        try
        {
            foreach (var levelPath in paths)
            {
#if UNITY_EDITOR
                if (levelPath.IndexOf("completedLevels.json") != -1)
                {
                    SetLevelInformation(Application.streamingAssetsPath + "/" + levelPath);
                    continue;
                }
#endif
                string levelName = ChangePanelScript.GetLastName(levelPath.Replace(".json", ""));
                NPCQuestions questions = GetNpcQuestions(levelPath, isLink);
                if (questions != null)
                {
                    PanelButton panelButton = ChangePanelScript.CreateButton("Пройти этаж: " + ChangePanelScript.GetLastName(levelName),
                        button,
                        panel,
                        null,
                        () => AudioController.Instance.PlayOneAudio(nextSceneAudio),
                        () => DialogScript.GetQuestions += () => questions,
                        () => SceneChangeScript.Instance.ChangeScene(gameParameters.FloorSceneName),
                        () => DialogScript.Path = levelPath);
                    SetCompletedButton(panelButton, ChangePanelScript.GetLastName(levelName), gameParameters.PassedLevelColor);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        Debug.Log("---------------------------------------------");
        Debug.Log("levelsInformation : " + levelsInformation.Count + " \n " + " PATH : " + Application.persistentDataPath);
        Debug.Log("---------------------------------------------");
    }
    private static void SetLevelInformation(string path)
    {
        Debug.Log("SetLevelInformation" + path);
        try
        {
            using (var stream = File.Open(path, FileMode.OpenOrCreate))
            {
                levelsInformation = FileOperations.ReadJsonWithTypes<List<LevelInformation>>(FileEncryption.ReadBytes(FileOperations.GetBytesInStream(stream)));
            }
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
