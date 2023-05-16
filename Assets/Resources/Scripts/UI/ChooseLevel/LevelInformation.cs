using Newtonsoft.Json;
using System;

[Serializable]
public class LevelInformation
{
    [JsonProperty("levelCompletedName")]
    public string LevelCompletedName { get; set; }
    
    [JsonProperty("levelRecord")]
    public int LevelRecord { get; set; }

    public override string ToString()
    {
        return "Completed name: " + LevelCompletedName + "\n"
            + "Record: " + LevelRecord + "\n";
    }
}
