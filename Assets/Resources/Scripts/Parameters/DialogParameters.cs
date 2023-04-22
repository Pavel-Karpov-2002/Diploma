using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class DialogParameters
{
    [SerializeField] private GameObject buttonAnswer;
    [SerializeField] private TMP_FontAsset questionFontAsset;
    [SerializeField] private TMP_FontAsset buttonFontAsset;
    [SerializeField] private Color buttonTrueColor;
    [SerializeField] private Color buttonFalseColor;
    [SerializeField] private string url;
    [SerializeField] private string filePath;
    [SerializeField][Min(0)] private float timeToRespond;
    [SerializeField][Min(0)] private float timeAfterResponse;
    [SerializeField][Min(0)] private int amountQuestionPerOneNPC;

    public GameObject ButtonAnswer => buttonAnswer;
    public TMP_FontAsset QuestionFontAsset => questionFontAsset;
    public TMP_FontAsset ButtonFontAsset => buttonFontAsset;
    public Color ButtonTrueColor => buttonTrueColor;
    public Color ButtonFalseColor => buttonFalseColor;
    public string Url => url;
    public string FilePath => filePath;
    public float TimeToRespond => timeToRespond;
    public float TimeAfterResponse => timeAfterResponse;
    public int AmountQuestionPerOneNPC => amountQuestionPerOneNPC;
}
