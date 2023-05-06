using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCParameters", menuName = "CustomParameters/NPCParameters")]
public class NPCParameters : ScriptableObject
{
    [SerializeField] private List<Sprite> studentsSprites;
    [SerializeField] private List<Sprite> teachersSprites;
}
