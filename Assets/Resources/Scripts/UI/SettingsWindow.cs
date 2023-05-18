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
#if UNITY_EDITOR
        string path = Application.streamingAssetsPath;
#elif UNITY_ANDROID
        string path = Application.persistentDataPath;
# endif
        using (var stream = File.Open(path + gameParameters.SettingsPath, FileMode.OpenOrCreate))
        {
            settings = FileOperations.ReadJsonWithTypes<SettingsData>(new StreamReader(stream).ReadLine());
            AudioListener.volume = settings.SoundVolume;
            slider.value = settings.SoundVolume;
        }
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
#if UNITY_EDITOR
        using (var stream = File.Open(Application.streamingAssetsPath + gameParameters.SettingsPath, FileMode.OpenOrCreate))
        {
            string jsonString = JsonConvert.SerializeObject(settings);
            byte[] bytes = Encoding.ASCII.GetBytes(jsonString);
            stream.SetLength(0);
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
        }
#elif UNITY_ANDROID
        using (var stream = File.Open(Application.persistentDataPath + gameParameters.SettingsPath, FileMode.OpenOrCreate))
        {
            string jsonString = JsonConvert.SerializeObject(settings);
            byte[] bytes = Encoding.ASCII.GetBytes(jsonString); 
            stream.SetLength(0);
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
        }
#endif
    }
}
