using System;
using UnityEngine;

[Serializable]
public class SkillsParameters
{
    [SerializeField][Min(0)] private int amountUseTimeSkill;
    [SerializeField][Min(0)] private int addedPercentTimeCountSkill;
    [SerializeField][Min(0)] private int amountAdditionalQuestions;
    [SerializeField][Min(0)] private int amountIncreasedPoints;
    [SerializeField][Min(0)] private int amountSkipQuestion;
    [SerializeField][Min(0)] private int amountUsesGetResponse;

    public int AmountUseTimeSkill => amountUseTimeSkill;
    public int AddedPercentTimeCountSkill => addedPercentTimeCountSkill;
    public int AmountAdditionalQuestions => amountAdditionalQuestions;
    public int AmountIncreasedPoints => amountIncreasedPoints;
    public int AmountSkipQuestion => amountSkipQuestion;
    public int AmountUsesGetResponse => amountUsesGetResponse;
}
