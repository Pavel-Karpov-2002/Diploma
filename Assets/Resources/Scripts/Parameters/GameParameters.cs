using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Parameters", fileName = "GamePatameters")]
public class GameParameters : ScriptableObject
{
    [SerializeField] private NPCParameters npc;
    [SerializeField] private MazeParameters maze;
    [SerializeField] private DialogParameters dialog;
    [SerializeField] private PlayerParameters player;
    [SerializeField] private SkillsParameters skills;
    [SerializeField] private float timeLoadSceneAttenuation;
    [SerializeField] private ItemParameters itemStatusWindow;
    [SerializeField] private List<Item> items;

    public NPCParameters NPC => npc;
    public MazeParameters Maze => maze;
    public DialogParameters Dialog => dialog;
    public PlayerParameters Player => player;
    public SkillsParameters Skills => skills;
    public float TimeLoadSceneAttenuation => timeLoadSceneAttenuation;
    public ItemParameters ItemStatusWindow => itemStatusWindow;
    public List<Item> Items => items;
}
