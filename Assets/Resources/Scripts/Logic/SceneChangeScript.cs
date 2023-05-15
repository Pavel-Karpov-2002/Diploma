using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup), typeof(Image))]
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
        sequence = DOTween.Sequence();
    }

    private void Start()
    {
        canvasGroup.blocksRaycasts = true;
        fadeCoroutine = TimeAttenuation(0, gameParameters.TimeLoadSceneAttenuation);
        StartCoroutine(fadeCoroutine);
    }

    public void ChangeScene(string sceneName)
    {
        canvasGroup.blocksRaycasts = true;
        NextScene(1, gameParameters.TimeLoadSceneAttenuation, sceneName);
    }

    public void LoadGameData()
    {
        GameData data = FileReader.ReadJsonWithTypes<GameData>(FileEncryption.ReadFile(gameParameters.DataPath));
        if (data != null)
            GameData.Data = data;
        else
            GameData.Data = new GameData();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
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
