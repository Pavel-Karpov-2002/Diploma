using System.Collections;
using System.IO;
using System.Linq;
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
        string[] paths = new string[0];
        try
        {
            paths = Directory.GetFiles(path, "*.json").Select(file => file.Replace(path, linkParameters.FolderPath)).ToArray();
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
        if (paths.Length > 0)
            CreateButtonsLevel.CreateButtons(paths, linkPanel, linkButton, audioParameters.DoorOpen, gameParameters);
    }

    public void AddButton()
    {
        DownloaderFile.Instance.GetFile(linkInput.text);
#if UNITY_EDITOR
        string path = Application.streamingAssetsPath + linkParameters.FolderPath;
#elif UNITY_ANDROID             
        string path = Application.persistentDataPath + linkParameters.FolderPath;
#endif
        StartCoroutine(UpdateButtons(path));
    }

    private IEnumerator UpdateButtons(string path)
    {
        yield return new WaitForSeconds(0.5f);
        if (DownloaderFile.Instance.IsDone)
            CreateLinkButtons(path);
    }
}
