using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public class Item
{
    [SerializeReference] private Skill skill;
    [SerializeField] private string itemName;
    [SerializeField] private string itemInformation;
    [SerializeField] private string itemSpritePath;

    [JsonProperty("Skill")]
    public Skill Skill { get => skill; set => skill = value; }
    [JsonProperty("ItemSpritePath")]
    public string ItemSpritePath { get => itemSpritePath; set => itemSpritePath = value; }
    [JsonProperty("ItemName")]
    public string ItemName { get => itemName; set => itemName = value; }
    [JsonProperty("ItemInformation")]
    public string ItemInformation { get => itemInformation; set => itemInformation = value; }

    public override string ToString()
    {
        return "Skill: " + skill.ToString() + "\n"
            + "Item path: " + itemSpritePath + "\n"
            + "Item name: " + itemName + "\n"
            + "Item information: " + itemInformation +"\n";
    }
}
