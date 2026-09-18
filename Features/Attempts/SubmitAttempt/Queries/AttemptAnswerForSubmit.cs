namespace exam_system.Features.Attempts.SubmitAttempt.Queries
{
    public class AttemptAnswerForSubmit
    {
        public Guid AnswerId { get; set; }

        public Guid? SelectedOptionId { get; set; }

        public bool OptionIsCorrect { get; set; }

        public DateTime AnsweredAt { get; set; }
    }
}
