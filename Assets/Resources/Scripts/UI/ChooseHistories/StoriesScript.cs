using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class StoriesScript : MonoBehaviour
{
    [SerializeField] private GameObject storiesPanel;
    [SerializeField] private PanelButton storiesButton;
    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private GameObject textPanel;
    [SerializeField] private GameObject buttonPanel;

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
                    () => DemonstrationHistory(FileReader.ReadTextFile(path + storiesName + ".json")), 
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
}
