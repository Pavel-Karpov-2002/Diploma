using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Parameters", fileName = "GamePatameters")]
public class GameParameters : ScriptableObject
{
    [SerializeField] private float timeLoadSceneAttenuation;
    [SerializeField] private List<Item> items;

    public float TimeLoadSceneAttenuation => timeLoadSceneAttenuation;
    public List<Item> Items => items;
}
