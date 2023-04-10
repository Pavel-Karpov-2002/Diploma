using System;
using TMPro;
using UnityEngine;

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

    public GameObject ButtonAnswer => buttonAnswer;
    public TMP_FontAsset QuestionFontAsset => questionFontAsset;
    public TMP_FontAsset ButtonFontAsset => buttonFontAsset;
    public Color ButtonTrueColor => buttonTrueColor;
    public Color ButtonFalseColor => buttonFalseColor;
    public string Url => url;
    public string FilePath => filePath;
}
