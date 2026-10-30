namespace StudentsManager.Mvc.Domain.Views.UsersScore
{
    public class UserScoreView
    {
        public Guid UserId { get; set; }

        public decimal FirstExaminationScore { get; set; }

        public decimal SecondExaminationScore { get; set; }

        public decimal Points { get; set; }

        public decimal Total { get; set; }
    }
}
