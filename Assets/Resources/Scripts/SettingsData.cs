using Newtonsoft.Json;
using System;

[Serializable]
public class SettingsData
{
    [JsonProperty("SoundVolume")]
    public float SoundVolume { get; set; }
}
