using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FacultyPanelScript : MonoBehaviour
{
    [SerializeField] private FacultyParameters facultyParameters;
    [SerializeField] private FacultyButton facultyButton;
    [SerializeField] private GameObject facultyPanel;

    private static FacultyPanelScript instance;

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
        CreateButtons(Directory.GetFiles(filePath, "*.json"), null,
            () => DialogScript.GetQuestions += QuestionLogic.GetFileQuestions,
            () => SceneChangeScript.GetInstance().GoToFloor());
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

    private void CreateButtons(IEnumerable<string> pathes, NextPanelDelegate nextPanel, params ButtonActivateFunction[] buttonsActivate)
    {
        ClearFacultyPanel();
        try
        {
            foreach (var path in pathes)
            {
                FacultyButton button = CreateButton("Пройти этаж: " + GetLastName(path.Replace(".json", "")), nextPanel, () => DialogScript.Path = path, buttonsActivate);
                buttonsPool.Add(button);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
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
