namespace StudentsManager.Mvc.Domain.Entities
{
    public class PersonalityTestChoice
    {
        public int Id { get; set; }

        public int QuestionNumber { get; set; }
        public int AnswerNumber { get; set; }
        public PersonalityTrait Trait { get; set; }

        public DateTimeOffset CreatedAtUtc { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
