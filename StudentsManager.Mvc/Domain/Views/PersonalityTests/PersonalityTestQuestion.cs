namespace StudentsManager.Mvc.Domain.Views.PersonalityTests
{
    public record PersonalityTestQuestion(int Number, IReadOnlyList<PersonalityTestAnswer> Answers)
    {
        public bool HasImages => Answers.All(answer => answer.ImageFileName != null);
    }
}
