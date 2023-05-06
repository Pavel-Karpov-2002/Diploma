using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogParameters", menuName = "CustomParameters/DialogParameters")]
public class DialogParameters : ScriptableObject
{
    [SerializeField] private GameObject buttonAnswer;
    [SerializeField] private TMP_FontAsset questionFontAsset;
    [SerializeField] private TMP_FontAsset buttonFontAsset;
    [SerializeField] private Color buttonTrueColor;
    [SerializeField] private Color buttonFalseColor;
    [SerializeField][Min(0)] private float timeAfterResponse;

    public GameObject ButtonAnswer => buttonAnswer;
    public TMP_FontAsset QuestionFontAsset => questionFontAsset;
    public TMP_FontAsset ButtonFontAsset => buttonFontAsset;
    public Color ButtonTrueColor => buttonTrueColor;
    public Color ButtonFalseColor => buttonFalseColor;
    public float TimeAfterResponse => timeAfterResponse;
}
