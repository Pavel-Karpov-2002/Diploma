using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Parameters", fileName = "GamePatameters")]
public class GameParameters : ScriptableObject
{
    [SerializeField] private List<PeopleParameters> people;
    [SerializeField] private MazeParameters maze;
    [SerializeField] private DialogParameters dialog;

    public List<PeopleParameters> People => people;
    public MazeParameters Maze => maze;
    public DialogParameters Dialog => dialog;

}
