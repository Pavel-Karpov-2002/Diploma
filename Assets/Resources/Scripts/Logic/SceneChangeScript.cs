using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup), typeof(Image))]
public class SceneChangeScript : MonoBehaviour
{
    [SerializeField] private Image spriteAttenuation;
    [SerializeField] private GameParameters gameParameters;
    [SerializeField] private CanvasGroup canvasGroup;

    private const string mainMenu = "MainMenuScene";
    private const string floorScene = "FloorScene";
    private const string loseScene = "LoseScene";

    private Sequence sequence;
    private IEnumerator fadeCoroutine;
    private static SceneChangeScript instance;

    private void Awake()
    {
        sequence = DOTween.Sequence();
    }

    private void Start()
    {
        spriteAttenuation.gameObject.SetActive(true);
        canvasGroup.blocksRaycasts = true;
        fadeCoroutine = TimeAttenuation(0, gameParameters.TimeLoadSceneAttenuation);
        StartCoroutine(fadeCoroutine);
    }

    public void MainMenuScene()
    {
        NextSceneWithAttenuation(1, gameParameters.TimeLoadSceneAttenuation, mainMenu);
    }

    public void GoToFloor()
    {
        NextSceneWithAttenuation(1, gameParameters.TimeLoadSceneAttenuation, floorScene);
    }

    public void LoseScene()
    {
        NextSceneWithAttenuation(1, gameParameters.TimeLoadSceneAttenuation, loseScene);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }

    private void NextSceneWithAttenuation(float endTime, float duration, string sceneName)
    {
        fadeCoroutine = TimeAttenuation(endTime, duration);
        StartCoroutine(fadeCoroutine);
        StartCoroutine(SetSceneAfterAttenuation(duration, sceneName));
    }

    private IEnumerator TimeAttenuation(float endTime, float duration)
    {
        spriteAttenuation.DOFade(endTime, duration).SetEase(Ease.Linear);

        yield return new WaitForSeconds(duration);

        canvasGroup.blocksRaycasts = false;
    }

    private IEnumerator SetSceneAfterAttenuation(float duration, string sceneName)
    {
        yield return new WaitForSeconds(duration);

        SceneManager.LoadScene(sceneName);
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
