namespace StudentsManager.Mvc.Domain.Views
{
    public class TestQuestionView
    {
        /// <summary>
        ///     Test Question ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Test Question Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Test Question Options.
        /// </summary>
        public List<QuestionOptionView> Options { get; set; }
    }
}