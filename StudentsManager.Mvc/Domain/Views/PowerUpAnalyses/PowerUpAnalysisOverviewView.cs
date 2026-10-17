using StudentsManager.Mvc.Domain.Views.PersonalityTests;
using StudentsManager.Mvc.Domain.Views.Surveys;

namespace StudentsManager.Mvc.Domain.Views.PowerUpAnalyses
{
    public class PowerUpAnalysisOverviewView
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public SurveyResultView? Survey { get; set; }
        public PersonalityTestResultView? Personality { get; set; }
        public PowerUpAnalysisView? Analysis { get; set; }

        public bool CanGenerate => Survey != null && Personality != null;
    }
}
