using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StudentsManager.Mvc.Domain.Inputs.RoboExaminations;
using Xunit;

namespace StudentsManager.Tests.Services.RoboExaminations;

public class RoboExaminationInputTests
{
    [Fact]
    public void AcceptsValidTextQuestions()
    {
        Assert.Empty(Validate(ValidInput()));
    }

    [Fact]
    public void RequiresNonEmptyQuestionList()
    {
        var input = ValidInput();
        input.Questions.Clear();

        Assert.Contains(Validate(input), result => result.ErrorMessage!.Contains("minimum length"));
    }

    [Fact]
    public void RequiresNonEmptyUniqueQuestionIds()
    {
        var input = ValidInput();
        input.Questions[0].Id = "";
        input.Questions[1].Id = input.Questions[0].Id;

        var errors = Validate(input);

        Assert.Contains(errors, result => result.ErrorMessage!.Contains("ID must contain"));
        Assert.Contains(errors, result => result.ErrorMessage!.Contains("unique"));
    }

    [Fact]
    public void RequiresQuestionTextWithinLimit()
    {
        var input = ValidInput();
        input.Questions[0].Text = new string('x', 1001);
        input.Questions[1].Text = "  ";

        Assert.Equal(2, Validate(input).Count);
    }

    [Fact]
    public void RejectsUnsupportedQuestionType()
    {
        var input = ValidInput();
        input.Questions[0].Type = "options";

        Assert.Contains(Validate(input), result => result.ErrorMessage!.Contains("type must"));
    }

    private static List<ValidationResult> Validate(RoboExaminationInput input)
    {
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(input, new ValidationContext(input), results, validateAllProperties: true);
        return results;
    }

    private static RoboExaminationInput ValidInput() => new()
    {
        Type = " First ",
        Variant = " A ",
        Questions =
        [
            new RoboExaminationQuestion
            {
                Id = "q1",
                Text = "Explain block scope",
                Type = "text"
            },
            new RoboExaminationQuestion
            {
                Id = "q2",
                Text = "Explain the answer",
                Type = "text"
            }
        ]
    };
}
