using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FacultyPanelScript : Singleton<FacultyPanelScript>
{
    [SerializeField] private GameParameters gameParameters;
    [SerializeField] private AudioParameters audioParameters;
    [SerializeField] private FacultyParameters facultyParameters;
    [SerializeField] private PanelButton facultyButton;
    [SerializeField] private GameObject facultyPanel;

    public FacultyParameters FacultyParameters => facultyParameters;

    private void OnEnable()
    {
        CreateListOfFaculies(facultyParameters.FoldersPath);
    }

    public void CreateListOfFaculies(string path)
    {
        if (Directory.GetFiles(path, "*.json").Length > 0)
        {
            CreateButtonsLevel.CreateButtons(path, facultyPanel, facultyButton, audioParameters.DoorOpen, gameParameters);
            return;
        }
        ChangePanelScript.CreateButtonsInPanel(Directory.EnumerateDirectories(path),
            facultyButton,
            facultyPanel,
            CreateListOfFaculies);
    }
}
