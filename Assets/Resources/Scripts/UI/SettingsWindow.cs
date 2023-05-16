using Newtonsoft.Json;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject window;
    [SerializeField] private GameParameters gameParameters;

    private bool isChange;

    private SettingsData settings;

    private void Start()
    {
        settings = JsonConvert.DeserializeObject<SettingsData>(FileOperations.ReadTextFile(gameParameters.SettingsPath));
        AudioListener.volume = settings.SoundVolume;
        slider.value = settings.SoundVolume;
    }

    public void ChangeAudio()
    {
        isChange = true;
        AudioListener.volume = slider.value;
        settings.SoundVolume = slider.value;

    }

    public void ChangeWindowAcitve()
    {
        if (isChange)
        {
            SaveSettingsInFile();
        }

        window.SetActive(!window.activeInHierarchy);
    }

    public void ExitGame()
    {
        SaveSettingsInFile();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private void SaveSettingsInFile()
    {
        StreamWriter stream = new StreamWriter(gameParameters.SettingsPath);
        string jsonString = JsonConvert.SerializeObject(settings);
        stream.WriteLine(jsonString);
        stream.Close();
    }
}
