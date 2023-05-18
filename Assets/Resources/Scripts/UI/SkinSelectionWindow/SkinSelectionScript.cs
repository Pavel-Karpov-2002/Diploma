using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private bool isStart;

    private void Awake()
    {
        skinNumber = GetFileSkinNum();
        skins = npcSkins.Students;
        UseSkin();
    }

    private void OnEnable()
    {
        if (!isStart)
        {
            isStart = true;
            return;
        }
        AudioController.Instance.PlayOneAudio(audioParameters.OpenWardborde);
    }

    public void UseSkin()
    {
        AudioController.Instance.SetAudio(audioParameters.ChangeSkin);
        thisPlayer.GetComponent<SpriteRenderer>().sprite = skins[skinNumber].Skin;
        thisPlayer.GetComponent<Animator>().runtimeAnimatorController = skins[skinNumber].SkinAnimator;
        ShowItemInformation.Instance.PlayerSkin.sprite = skins[skinNumber].Skin;
        PlayerSkin.SkinNum = skinNumber;
        WriteFileSkinNum();
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

    private int GetFileSkinNum()
    {
#if UNITY_EDITOR
        string path = Application.streamingAssetsPath;
#elif UNITY_ANDROID             
        string path = Application.persistentDataPath;
#endif
        try
        { 
            return int.Parse(FileOperations.ReadTextFile(path + "/skinNum.txt"));
        }
        catch
        {
            return 0;
        }
    }

    private void WriteFileSkinNum()
    {
#if UNITY_EDITOR
        string path = Application.streamingAssetsPath;
#elif UNITY_ANDROID
        string path = Application.persistentDataPath;
#endif
        FileOperations.WriteTextFile(path + "/skinNum.txt", skinNumber.ToString());
    }
}
