using StudentsManager.Mvc.Domain.Entities;

namespace StudentsManager.Mvc.Domain.Views.PersonalityTests
{
    public class PersonalityTraitScoreView
    {
        public PersonalityTrait Trait { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Score { get; set; }
        public int MaxScore { get; set; }
        public decimal Percentage { get; set; }
        public bool IsStrongest { get; set; }
    }
}
