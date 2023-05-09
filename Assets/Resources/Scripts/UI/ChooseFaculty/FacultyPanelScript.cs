using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FacultyPanelScript : MonoBehaviour
{
    [SerializeField] private GameParameters gameParameters;
    [SerializeField] private FacultyParameters facultyParameters;
    [SerializeField] private FacultyButton facultyButton;
    [SerializeField] private GameObject facultyPanel;

    private static FacultyPanelScript instance;
    public static List<LevelInformation> levelsInformation;

    private delegate void NextPanelDelegate(string path);
    private delegate void ButtonActivateFunction();
    private List<FacultyButton> buttonsPool;

    public FacultyParameters FacultyParameters => facultyParameters;

    private void OnEnable()
    {
        buttonsPool = new List<FacultyButton>();
        CreateListOfFaculies(facultyParameters.FoldersPath);
    }

    public void CreateListOfFaculies(string path)
    {
        CreateButtons(Directory.EnumerateDirectories(path), CreateDifficultyButtons);
    }

    private void CreateDifficultyButtons(string facultyFolderPath)
    {
        CreateButtons(Directory.EnumerateDirectories(facultyFolderPath), CreateClassesButtons);
    }

    private void CreateClassesButtons(string facultyFolderPath)
    {
        CreateButtons(Directory.EnumerateDirectories(facultyFolderPath), CreateFloorsButtons);
    }

    private void CreateFloorsButtons(string filePath)
    {
        CreateButtonsLevels(filePath, null,
            () => DialogScript.GetQuestions += SerializeContent.GetFromJsonFile<NPCQuestions>,
            () => SceneChangeScript.GetInstance().ChangeScene(gameParameters.FloorSceneName));
    }

    private void CreateButtons(IEnumerable<string> pathes, NextPanelDelegate nextPanel)
    {
        ClearFacultyPanel();
        try
        {
            foreach (var path in pathes)
            {
                FacultyButton button = CreateButton(path, nextPanel);
                buttonsPool.Add(button);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private void CreateButtonsLevels(string path, NextPanelDelegate nextPanel, params ButtonActivateFunction[] buttonsActivate)
    {
        ClearFacultyPanel();
        try
        {
            IEnumerable<string> pathes = Directory.GetFiles(path, "*.json");
            string completedPathes = Directory.GetFiles(path, "completedLevels.json")[0];
            levelsInformation = SerializeContent.GetFromJsonFile<List<LevelInformation>>(completedPathes);

            foreach (var levelPath in pathes)
            {
                if (levelPath.Equals(completedPathes))
                    continue;

                string levelName = GetLastName(levelPath.Replace(".json", ""));

                FacultyButton button = CreateButton(
                        "Пройти этаж: " + GetLastName(levelName),
                        nextPanel,
                        () => DialogScript.Path = levelPath, buttonsActivate);
                SetCompletedButton(button, levelName);
                buttonsPool.Add(button);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private void SetCompletedButton(FacultyButton button, string levelName)
    {
        foreach (var levelInformation in levelsInformation)
        {
            if (levelInformation.LevelCompletedName.Equals(levelName))
            {
                button.Button.image.color = gameParameters.PassedLevelColor;
                button.FacultyText.text += "\n Рекорд: (" + levelInformation.LevelRecord + ") ";
            }
        }
    }

    private void ClearFacultyPanel()
    {
        buttonsPool.Clear();
        foreach (Transform children in facultyPanel.GetComponent<Transform>())
        {
            Destroy(children.gameObject);
        }
    }

    private FacultyButton CreateButton(string path, NextPanelDelegate nextPanel, ButtonActivateFunction staticPath = null, params ButtonActivateFunction[] functions)
    {
        FacultyButton button = Instantiate(facultyButton, facultyPanel.transform);
        button.Button.onClick.AddListener(() => button.Button.interactable = false);

        if (nextPanel != null)
            button.Button.onClick.AddListener(() => nextPanel?.Invoke(path));

        if (staticPath != null)
            button.Button.onClick.AddListener(() => staticPath?.Invoke());

        foreach (var func in functions)
            button.Button.onClick.AddListener(() => func?.Invoke());

        button.FacultyText.text = GetLastName(path);
        return button;
    }

    private string GetLastName(string subject)
    {
        return subject.Substring(subject.LastIndexOf(Path.DirectorySeparatorChar) + 1);
    }

    public static FacultyPanelScript GetInstance()
    {
        if (instance == null)
            instance = Resources.FindObjectsOfTypeAll<FacultyPanelScript>()[0];
        return instance;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
