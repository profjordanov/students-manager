using System;
using System.Linq;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Services.PersonalityTests;
using Xunit;

namespace StudentsManager.Tests.Services.PersonalityTests;

public class PersonalityTestConstantsTests
{
    [Fact]
    public void QuestionsAreNumberedFromOneToForty()
    {
        Assert.Equal(Enumerable.Range(1, 40), PersonalityTestConstants.Questions.Select(question => question.Number));
    }

    [Fact]
    public void EveryQuestionOffersTwoDifferentTraits()
    {
        Assert.All(PersonalityTestConstants.Questions, question =>
        {
            Assert.Equal(2, question.Answers.Count);
            Assert.NotEqual(question.Answers[0].Trait, question.Answers[1].Trait);
        });
    }

    [Fact]
    public void EveryTraitCanBeChosenTwentyTimes()
    {
        var answersPerTrait = PersonalityTestConstants.Questions
            .SelectMany(question => question.Answers)
            .GroupBy(answer => answer.Trait)
            .ToDictionary(group => group.Key, group => group.Count());

        Assert.All(Enum.GetValues<PersonalityTrait>(), trait => Assert.Equal(20, answersPerTrait[trait]));
    }
}
