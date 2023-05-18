using System;
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

    private void OnEnable()
    {
#if UNITY_EDITOR
        CreateListButtons(gameParameters.StoriesPath);
#elif UNITY_ANDROID             
        CreateListButtons(gameParameters.StoriesPath);
#endif
        buttonPanel.SetActive(true);
    }

    public void CreateListButtons(string path)
    {
        try
        {
            string[] paths = BetterStreamingAssets.GetFiles(path, "*.json");
            CreateButtonsLevels(paths);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private void CreateButtonsLevels(string[] paths)
    {
        try
        {
            ChangePanelScript.ClearPanel(storiesPanel);
            foreach (var storiesPaths in paths)
            {
                string storiesName = ChangePanelScript.GetLastName(storiesPaths).Replace(".json", "");
                PanelButton button = ChangePanelScript.CreateButton(storiesPaths,
                    storiesButton, storiesPanel, null,
                    () => AudioController.Instance.PlayOneAudio(audioParameters.PageRustling),
                    () => DemonstrationHistory(BetterStreamingAssets.ReadAllText(storiesPaths)),
                    () => ChangeActivePanel(false));
                button.ButtonText.text = storiesName;
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
