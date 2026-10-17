using StudentsManager.Mvc.Domain.Entities;

namespace StudentsManager.Mvc.Domain.Views.PersonalityTests
{
    public record PersonalityTestAnswer(string Text, PersonalityTrait Trait, string? ImageFileName = null);
}
