using Newtonsoft.Json;

public class NPCQuestionsInformation
{
    [JsonProperty("questions")]
    public Question[] Questions { get; set; }

    [JsonProperty("amountQuestionsForTest")]
    public int AmountQuestionsForTest { get; set; }

    public override string ToString()
    {
        return "questions count: " + Questions.Length + "\n" +
            "Amount: " + AmountQuestionsForTest;
    }
}
