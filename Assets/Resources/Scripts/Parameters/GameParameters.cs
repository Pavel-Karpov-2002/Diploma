using UnityEngine;

[CreateAssetMenu(menuName = "Parameters", fileName = "GamePatameters")]
public class GameParameters : ScriptableObject
{
    [SerializeField] private NPCParameters npc;
    [SerializeField] private MazeParameters maze;
    [SerializeField] private DialogParameters dialog;
    [SerializeField] private PlayerParameters player;
    [SerializeField] private float timeLoadSceneAttenuation;

    public NPCParameters NPC => npc;
    public MazeParameters Maze => maze;
    public DialogParameters Dialog => dialog;
    public PlayerParameters Player => player;
    public float TimeLoadSceneAttenuation => timeLoadSceneAttenuation;
}
