using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Animator))]
public class SkinSelectionScript : MonoBehaviour
{
    [SerializeField] private AudioParameters audioParameters;
    [SerializeField] private NPCParameters npcSkins;
    [SerializeField] private Image skinWindowDemostration;
    [SerializeField] private Animator skinWindowAnimator;
    [SerializeField] private GameObject thisPlayer;
    [SerializeField] private string PlayerNamePrefab;

    private List<NPCSkin> skins;
    private int skinNumber;
    private GameObject player;

    private void Start()
    {
        if (skinWindowDemostration == null)
            skinWindowDemostration = GetComponent<Image>();
        if (skinWindowAnimator == null)
            skinWindowAnimator = GetComponent<Animator>();
        skins = npcSkins.Students;
        ChangeSelection(true);
        player = Resources.Load<GameObject>("Prefabs/Units/" + PlayerNamePrefab);
    }

    private void OnEnable()
    {
        AudioController.Instance.PlayOneAudio(audioParameters.OpenWardborde);
    }

    public void UseSkin()
    {
        AudioController.Instance.SetAudio(audioParameters.ChangeSkin);
        thisPlayer.GetComponent<SpriteRenderer>().sprite = skins[skinNumber].Skin;
        thisPlayer.GetComponent<Animator>().runtimeAnimatorController = skins[skinNumber].SkinAnimator;
        player.GetComponent<SpriteRenderer>().sprite = skins[skinNumber].Skin;
        player.GetComponent<Animator>().runtimeAnimatorController = skins[skinNumber].SkinAnimator;
    }

    public void ChangeSelection(bool isNext)
    {
        AudioController.Instance.SetAudio(audioParameters.OpeningInventory);
        skinNumber += (isNext ? 1 : -1);
        if (skinNumber < 0)
            skinNumber = skins.Count - 1;
        if (skinNumber >= skins.Count)
            skinNumber = 0;
        skinWindowDemostration.sprite = skins[skinNumber].Skin;
        skinWindowAnimator.runtimeAnimatorController = skins[skinNumber].SkinAnimator;
    }
}
