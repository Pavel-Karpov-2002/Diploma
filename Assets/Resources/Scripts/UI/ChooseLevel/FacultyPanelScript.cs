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
    string path;

    protected override void Awake()
    {
        base.Awake();
#if UNITY_EDITOR
        path = Application.streamingAssetsPath + facultyParameters.FoldersPath;
#elif UNITY_ANDROID             
        path = Application.persistentDataPath + facultyParameters.FoldersPath;
#endif
        CreateDirectories(path);
    }

    private void OnEnable()
    {
#if UNITY_EDITOR
        CreateListOfFaculies(path);
#elif UNITY_ANDROID             
        CreateListOfFaculies(path);
#endif
    }

    public void CreateListOfFaculies(string path)
    {
        ChangePanelScript.CreateButtonsInPanel(Directory.EnumerateDirectories(path),
            facultyButton,
            facultyPanel,
            CreateListOfFaculies);
        if (Application.persistentDataPath + facultyParameters.FoldersPath == path)
            return;
#if UNITY_EDITOR
        string[] paths = BetterStreamingAssets.GetFiles(path.Replace(Application.streamingAssetsPath, "") + "/", "*.json");
#elif UNITY_ANDROID
        string[] paths = BetterStreamingAssets.GetFiles(path.Replace(Application.persistentDataPath, "") + "/", "*.json");
#endif
        if (paths.Length > 0)
            CreateButtonsLevel.CreateButtons(paths, facultyPanel, facultyButton, audioParameters.DoorOpen, gameParameters);
    }

    private void CreateDirectories(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        foreach (var facultie in facultyParameters.Faculties)
        {
            string namePath = path + facultie.Name + "/";
            if (!Directory.Exists(namePath))
                Directory.CreateDirectory(namePath);
            foreach (var discipline in facultie.Discilines)
            {
                if (!Directory.Exists(namePath + discipline))
                    Directory.CreateDirectory(namePath + discipline + "/");
            }
        }
    }
}
