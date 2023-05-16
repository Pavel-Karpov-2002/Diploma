using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameException : Singleton<GameException>
{
    [SerializeField] private TextMeshProUGUI exeptionText;
    [SerializeField] private CanvasGroup exeptionPanel;
    [SerializeField] private AudioParameters audioParameters;
    [SerializeField] private GameParameters gameParameters;
    Sequence sequence;

    private void Start()
    {
        sequence = DOTween.Sequence();
        exeptionPanel.gameObject.SetActive(true);
        exeptionPanel.alpha = 0;
    }

    public void ShowError(string exeption)
    {
        sequence.Kill();
        sequence = DOTween.Sequence();
        AudioController.Instance.PlayOneAudio(audioParameters.Warning);
        exeptionPanel.alpha = 1;
        exeptionText.text = exeption;
        sequence.PrependInterval(gameParameters.TimeExeptionAttenuation)
            .Append(exeptionPanel.DOFade(0, gameParameters.TimeExeptionDuration));
    }
}
