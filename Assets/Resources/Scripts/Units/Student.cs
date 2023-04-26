public class Student : NPC
{
    public override void SetExpectation(bool expectation)
    {
        IsExpectation = expectation;
        AnswerButton.ChangeColorButton = true;
        DialogScript.FilePath = gameParameters.Dialog.FilePathForStudents;
        DialogScript.UrlSiteQuestion = gameParameters.Dialog.UrlForStudents;
        AnswerButton.OnPlayerAnswered += ChangeScores;
        NewQuestions(gameParameters.Dialog.FilePathForStudents, gameParameters.Dialog.UrlForStudents);
    }

    public void ChangeScores(bool isCorrectAnswer)
    {
        ((PlayerScores)PlayerConstructor.GetInstance()).ChangeScores(isCorrectAnswer);
    }
}
