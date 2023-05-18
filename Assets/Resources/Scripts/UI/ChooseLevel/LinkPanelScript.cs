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
#if UNITY_EDITOR
        CreateLinkButtons(Application.streamingAssetsPath + linkParameters.FolderPath);
#elif UNITY_ANDROID             
        CreateLinkButtons(Application.persistentDataPath + linkParameters.FolderPath);
#endif
    }

    public void CreateLinkButtons(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        string[] paths = Directory.GetFiles(path, "*.json");
        if (paths.Length > 0)
            CreateButtonsLevel.CreateButtons(paths, linkPanel, linkButton, audioParameters.DoorOpen, gameParameters, true);
    }

    public void AddButton()
    {
        DownloaderFile.Instance.GetFile(linkInput.text);
        StartCoroutine(UpdateButtons(Application.persistentDataPath + linkParameters.FolderPath));
    }

    private IEnumerator UpdateButtons(string path)
    {
        yield return new WaitForSeconds(0.5f);
        if (countAttempts == 10)
            yield return null;
        if (DownloaderFile.Instance.IsDone)
        {
            CreateLinkButtons(path);
        }
        countAttempts++;
        StartCoroutine(UpdateButtons(path));
    }
}
