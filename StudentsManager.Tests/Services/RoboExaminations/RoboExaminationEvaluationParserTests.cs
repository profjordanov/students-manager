using StudentsManager.Mvc.Services.RoboExaminations;
using Xunit;

namespace StudentsManager.Tests.Services.RoboExaminations;

public class RoboExaminationEvaluationParserTests
{
    [Fact]
    public void ParsesValidJson()
    {
        const string raw = """{"grade":5,"overallFeedback":"Good work."}""";

        var result = RoboExaminationEvaluationParser.Parse(raw);

        Assert.Equal(5, result.Grade);
        Assert.Equal("Good work.", result.Feedback);
        Assert.Equal(raw, result.Raw);
    }

    [Fact]
    public void ParsesFencedJson()
    {
        var result = RoboExaminationEvaluationParser.Parse(
            "```json\n{\"grade\":4,\"overallFeedback\":\"Clear answer.\"}\n```");

        Assert.Equal(4, result.Grade);
        Assert.Equal("Clear answer.", result.Feedback);
    }

    [Theory]
    [InlineData("The grade is 5.")]
    [InlineData("""{"grade":1,"overallFeedback":"Invalid"}""")]
    [InlineData("""{"grade":7,"overallFeedback":"Invalid"}""")]
    [InlineData("""{"grade":5,"overallFeedback":"  "}""")]
    public void RejectsInvalidResponseAndPreservesRaw(string raw)
    {
        var exception = Assert.Throws<RoboExaminationEvaluationException>(
            () => RoboExaminationEvaluationParser.Parse(raw));

        Assert.Equal(raw, exception.Raw);
    }
}
