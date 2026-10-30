using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StudentsManager.Mvc.Domain.Views.RoboExaminations;
using Xunit;

namespace StudentsManager.Tests.Services.RoboExaminations;

public class RoboExaminationSerializationTests
{
    [Fact]
    public void ResultStatusIsAStringAndQuestionViewsDoNotExposeExpectedAnswers()
    {
        var result = new RoboExaminationResultView
        {
            EvaluationStatus = "Succeeded",
            Answers = [new RoboExaminationAnswerView { QuestionId = "q1", QuestionText = "Question", Answer = "Answer" }]
        };
        var examination = new RoboExaminationView
        {
            Questions = [new RoboExaminationQuestionView { Id = "q1", Text = "Question", Type = "text" }]
        };

        var settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        var resultJson = JsonConvert.SerializeObject(result, settings);
        var examinationJson = JsonConvert.SerializeObject(examination, settings);

        Assert.Contains("\"evaluationStatus\":\"Succeeded\"", resultJson);
        Assert.DoesNotContain("expectedAnswer", examinationJson.ToLowerInvariant());
    }
}
