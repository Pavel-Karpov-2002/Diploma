using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCParameters", menuName = "CustomParameters/NPCParameters")]
public class NPCParameters : ScriptableObject
{
    [SerializeField] private List<NPCSkin> students;
    [SerializeField] private List<NPCSkin> teachers;

    public List<NPCSkin> Students => students;
    public List<NPCSkin> Teachers => teachers;
}
