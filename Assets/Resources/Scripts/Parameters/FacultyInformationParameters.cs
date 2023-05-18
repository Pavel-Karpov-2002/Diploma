using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FacultyInformationParameters
{
    [SerializeField] private string name;
    [SerializeField] private List<string> discilines;
    [SerializeField] private Sprite facultyImage;
    
    public string Name => name;
    public List<string> Discilines => discilines;
    public Sprite FacultyImage => facultyImage;
}
