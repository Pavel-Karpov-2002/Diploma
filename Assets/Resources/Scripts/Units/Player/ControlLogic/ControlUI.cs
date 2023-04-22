using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlUI : MonoBehaviour
{
    [SerializeField] private GameParameters gameParameters;
    [SerializeField] private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
        StartCoroutine(ChangeAlphaControl());
    }

    private IEnumerator ChangeAlphaControl()
    {
        yield return new WaitForSeconds(gameParameters.TimeLoadSceneAttenuation);
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
}
