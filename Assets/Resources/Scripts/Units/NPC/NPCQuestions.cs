using Newtonsoft.Json;

public class NPCQuestions
{
    [JsonProperty("minPercentCorrectTeacher")]
    public int MinPercentCorrectTeacher { get; set; }

    [JsonProperty("minPercentCorrectToUnlockTeacher")]
    public int MinPercentCorrectToUnlockTeacher { get; set; }

    [JsonProperty("amountStudentsOnFloor")]
    public int AmountStudentsOnFloor { get; set; }

    [JsonProperty("student")]
    public NPCQuestionsInformation Student { get; set; }

    [JsonProperty("teacher")]
    public NPCQuestionsInformation Teacher { get; set; }

    public override string ToString()
    {
        return "Min percentage of correct of teacher" + MinPercentCorrectTeacher + "\n"
            + "Student: " + Student.ToString() + "\n"
            + "Teacher: " + Teacher.ToString() + "\n";
    }
}

public enum NPCType
{
    Student,
    Teacher
}
