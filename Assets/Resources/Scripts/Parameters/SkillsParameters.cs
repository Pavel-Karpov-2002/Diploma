using UnityEngine;

[CreateAssetMenu(fileName = "Skills", menuName = "CustomParameters/Skills/SkillsParameters")]
public class SkillsParameters : ScriptableObject
{
    [SerializeField][Min(0)] private int amountUseTimeSkill;
    [SerializeField][Min(0)] private int addedPercentTimeCountSkill;
    [SerializeField][Min(0)] private int amountAdditionPercentagelSpeed;
    [SerializeField][Min(0)] private int amountIncreasedPointsInPercentage;
    [SerializeField][Min(0)] private int amountSkipQuestion;
    [SerializeField][Min(0)] private int amountUsesGetResponse;

    public int AmountUseTimeSkill => amountUseTimeSkill;
    public int AddedPercentTimeCountSkill => addedPercentTimeCountSkill;
    public int AmountAdditionPercentagelSpeed => amountAdditionPercentagelSpeed;
    public int AmountIncreasedPointsInPercentage => amountIncreasedPointsInPercentage;
    public int AmountSkipQuestion => amountSkipQuestion;
    public int AmountUsesGetResponse => amountUsesGetResponse;
}
