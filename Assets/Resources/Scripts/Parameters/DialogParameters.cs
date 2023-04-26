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
    [SerializeField] private string urlForStudents;
    [SerializeField] private string filePathForStudents;
    [SerializeField] private string urlForTeachers;
    [SerializeField] private string filePathForTeachers;
    [SerializeField][Min(0)] private float timeToRespond;
    [SerializeField][Min(0)] private float timeAfterResponse;
    [SerializeField][Min(0)] private int amountQuestionPerOneNPC;

    public GameObject ButtonAnswer => buttonAnswer;
    public TMP_FontAsset QuestionFontAsset => questionFontAsset;
    public TMP_FontAsset ButtonFontAsset => buttonFontAsset;
    public Color ButtonTrueColor => buttonTrueColor;
    public Color ButtonFalseColor => buttonFalseColor;
    public string UrlForStudents => urlForStudents;
    public string FilePathForStudents => filePathForStudents;
    public string UrlForTeachers => urlForTeachers;
    public string FilePathForTeachers => filePathForTeachers;
    public float TimeToRespond => timeToRespond;
    public float TimeAfterResponse => timeAfterResponse;
    public int AmountQuestionPerOneNPC => amountQuestionPerOneNPC;
}
