using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;

public class LinkPanelScript : Singleton<LinkPanelScript>
{
    [SerializeField] private GameParameters gameParameters;
    [SerializeField] private LinkParameters linkParameters;
    [SerializeField] private AudioParameters audioParameters;
    [SerializeField] private PanelButton linkButton;
    [SerializeField] private GameObject linkPanel;
    [SerializeField] private TMP_InputField linkInput;

    private int countAttempts;

    private void OnEnable()
    {
        CreateLinkButtons();
    }

    public void CreateLinkButtons()
    {
        string path = linkParameters.FolderPath;
        if (Directory.GetFiles(path, "*.json").Length > 0)
        {
            CreateButtonsLevel.CreateButtons(path, linkPanel, linkButton, audioParameters.DoorOpen, gameParameters);
            return;
        }
    }

    public void AddButton()
    {
        DownloaderFile.Instance.GetFile(linkInput.text);
        StartCoroutine(UpdateButtons());
    }

    private IEnumerator UpdateButtons()
    {
        yield return new WaitForSeconds(0.5f);
        if (countAttempts == 10)
            yield return null;
        if (DownloaderFile.Instance.IsDone)
        {
            CreateLinkButtons();
        }
        countAttempts++;
        StartCoroutine(UpdateButtons());
    }
}
