using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FacultyParameters", menuName = "CustomParameters/Faculty")]
public class FacultyParameters : ScriptableObject
{
    [SerializeField] private string foldersPath;
    [SerializeField] private float distanceActivateFacultyPanel;
    [SerializeField] private LayerMask layerActivateFacultyPanel;
    [SerializeField] private List<string> facultyName;
    
    public string FoldersPath => foldersPath;
    public float DistanceActivateFacultyPanel => distanceActivateFacultyPanel;
    public LayerMask LayerActivateFacultyPanel => layerActivateFacultyPanel;
    public List<string> FacultyName => facultyName;
}
