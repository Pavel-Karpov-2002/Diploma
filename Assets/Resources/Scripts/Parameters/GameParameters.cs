using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Parameters", fileName = "GamePatameters")]
public class GameParameters : ScriptableObject
{
    [SerializeField] private List<PeopleParameters> people;
    [SerializeField] private MazeParameters maze;
    [SerializeField] private DialogParameters dialog;
    [SerializeField] private PlayerParameters player;
    [SerializeField] private float timeLoadSceneAttenuation;

    public List<PeopleParameters> People => people;
    public MazeParameters Maze => maze;
    public DialogParameters Dialog => dialog;
    public PlayerParameters Player => player;
    public float TimeLoadSceneAttenuation => timeLoadSceneAttenuation;
}
