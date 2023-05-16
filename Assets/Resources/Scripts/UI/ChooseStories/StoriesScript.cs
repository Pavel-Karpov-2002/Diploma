using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class StoriesScript : Singleton<StoriesScript>
{
    [SerializeField] private AudioParameters audioParameters;
    [SerializeField] private GameParameters gameParameters;
    [SerializeField] private PanelButton storiesButton;
    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private GameObject storiesPanel;
    [SerializeField] private GameObject textPanel;
    [SerializeField] private GameObject buttonPanel;

    private void Start()
    {
        CreateListButtons(gameParameters.StoriesPath);
    }

    private void OnEnable()
    {
        buttonPanel.SetActive(true);
    }

    public void CreateListButtons(string path)
    {
        if (Directory.GetFiles(path, "*.json").Length > 0)
        {
            CreateButtonsLevels(path);
        }
    }

    private void CreateButtonsLevels(string path)
    {
        ChangePanelScript.ClearPanel(storiesPanel);

        try
        {
            IEnumerable<string> pathes = Directory.GetFiles(path, "*.json");
            foreach (var storiesPathes in pathes)
            {
                string lastName = ChangePanelScript.GetLastName(storiesPathes.Replace(".json", ""));
                string storiesName = ChangePanelScript.GetLastName(lastName);
                PanelButton button = ChangePanelScript.CreateButton(storiesName,
                    storiesButton, storiesPanel, null,
                    () => AudioController.Instance.PlayOneAudio(audioParameters.PageRustling),
                    () => DemonstrationHistory(FileOperations.ReadTextFile(path + storiesName + ".json")), 
                    () => ChangeActivePanel(false));
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private void ChangeActivePanel(bool isButtonActive)
    {
        buttonPanel.SetActive(isButtonActive);
        textPanel.SetActive(!isButtonActive);
    }

    private void DemonstrationHistory(string story)
    {
        storyText.text = story;
    }

    private void OnDisable()
    {
        textPanel.SetActive(false);
    }
}
