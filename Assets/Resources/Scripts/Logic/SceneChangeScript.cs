using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangeScript : MonoBehaviour
{
    [SerializeField] private GameParameters gameParameters;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Slider loaderBar;

    private Sequence sequence;
    private IEnumerator fadeCoroutine;
    private static SceneChangeScript instance;

    private void Awake()
    {
        BetterStreamingAssets.Initialize();
        sequence = DOTween.Sequence();
    }

    private void Start()
    {
        fadeCoroutine = TimeAttenuation(0, gameParameters.TimeLoadSceneAttenuation);
        StartCoroutine(fadeCoroutine);
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
#endif
    }

    public void ChangeScene(string sceneName)
    {
        canvasGroup.blocksRaycasts = true;
        NextScene(1, gameParameters.TimeLoadSceneAttenuation, sceneName);
    }

    public void LoadGameData()
    {
        GameData data = null;
#if UNITY_EDITOR
        using (var stream = File.Open(Application.streamingAssetsPath + gameParameters.DataPath, FileMode.OpenOrCreate))
        {
            data = FileOperations.ReadJsonWithTypes<GameData>(FileEncryption.ReadBytes(FileOperations.GetBytesInStream(stream)));
        }
#elif UNITY_ANDROID
        using (var stream = File.Open(Application.persistentDataPath + gameParameters.DataPath, FileMode.OpenOrCreate))
        {
            data = FileOperations.ReadJsonWithTypes<GameData>(FileEncryption.ReadBytes(FileOperations.GetBytesInStream(stream)));
        }
#endif
        if (data != null)
            GameData.Data = data;
        else
            GameData.Data = new GameData();
    }

    private void NextScene(float endTime, float duration, string sceneName)
    {
        fadeCoroutine = TimeAttenuation(endTime, duration);
        StartCoroutine(fadeCoroutine);
        StartCoroutine(SetSceneAfterAttenuation(duration, sceneName));
    }

    private IEnumerator TimeAttenuation(float endTime, float duration)
    {
        canvasGroup.DOFade(endTime, duration).SetEase(Ease.Linear);

        yield return new WaitForSeconds(duration);

        canvasGroup.blocksRaycasts = false;
    }

    private IEnumerator SetSceneAfterAttenuation(float duration, string sceneName)
    {
        yield return new WaitForSeconds(duration);

        StartCoroutine(LoadSync(sceneName));
    }

    private IEnumerator LoadSync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            loaderBar.value = asyncLoad.progress;
            yield return null;
        }
    }

    public static SceneChangeScript GetInstance()
    {
        if (instance == null)
            instance = Resources.FindObjectsOfTypeAll<SceneChangeScript>()[0];

        return instance;
    }

    private void OnDisable()
    {
        sequence.Kill();
        instance = null;
    }
}
