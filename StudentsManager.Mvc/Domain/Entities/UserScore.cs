using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsManager.Mvc.Domain.Entities
{
    public class UserScore
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public decimal FirstExaminationScore { get; set; }

        public decimal SecondExaminationScore { get; set; }

        public decimal Points { get; set; }

        [NotMapped]
        public decimal Total => FirstExaminationScore + SecondExaminationScore + Points;
    }
}
