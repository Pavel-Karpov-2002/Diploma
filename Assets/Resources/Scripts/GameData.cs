using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public static GameData Data { get; set; } = new GameData();

    [JsonProperty("PlayerItems")]
    public List<Item> PlayerItems { get; set; } = new List<Item>();

    [JsonProperty("AmountMoney")]   
    public int AmountMoney { get; set; }

    [JsonProperty("AmountPassedLevels")]
    public int AmountPassedLevels { get; set; }

    public override string ToString()
    {
        return "Player items count: " + PlayerItems.Count + "\n"
            + "Amount money: " + AmountMoney + "\n"
            + "Record passed levels: " + AmountPassedLevels + "\n";
    }
}