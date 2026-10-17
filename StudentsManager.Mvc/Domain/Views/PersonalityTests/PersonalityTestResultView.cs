namespace StudentsManager.Mvc.Domain.Views.PersonalityTests
{
    public class PersonalityTestResultView
    {
        public IReadOnlyList<PersonalityTraitScoreView> Scores { get; set; } = [];

        public int? FeedbackRating { get; set; }
        public string? FeedbackComment { get; set; }
    }
}
