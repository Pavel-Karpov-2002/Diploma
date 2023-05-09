using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Parameters", fileName = "GamePatameters")]
public class GameParameters : ScriptableObject
{
    [SerializeField] private float timeLoadSceneAttenuation;
    [SerializeField] private List<Item> items;
    [SerializeField] private string dataPath;
    [SerializeField] private string lobbySceneName;
    [SerializeField] private string floorSceneName;
    [SerializeField] private Color passedLevelColor;

    public float TimeLoadSceneAttenuation => timeLoadSceneAttenuation;
    public List<Item> Items => items;
    public string DataPath => dataPath;
    public string LobbySceneName => lobbySceneName;
    public string FloorSceneName => floorSceneName;
    public Color PassedLevelColor => passedLevelColor;
}
