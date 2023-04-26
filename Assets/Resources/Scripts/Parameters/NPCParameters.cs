using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NPCParameters
{
    [SerializeField] private List<Sprite> studentsSprites;
    [SerializeField] private List<Sprite> teachersSprites;

    [SerializeField][Min(0)] private int additionalNPCTroughtFloor;
    [SerializeField][Min(0)] private float percentageOfCorrectAnswers;
    [SerializeField][Min(0)] private float minPercentageOfCompletion;

    public List<Sprite> StudentsSprites => studentsSprites;
    public List<Sprite> TeachersSprites => teachersSprites;

    public int AdditionalNPCTroughtFloor => additionalNPCTroughtFloor;
    public float PercentOfCorrectAnswers => percentageOfCorrectAnswers;
    public float MinPercentageOfCompletion => minPercentageOfCompletion;
}
