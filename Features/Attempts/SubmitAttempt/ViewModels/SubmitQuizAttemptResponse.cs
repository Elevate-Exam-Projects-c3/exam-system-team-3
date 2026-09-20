namespace exam_system.Features.Attempts.SubmitAttempt.ViewModels
{
    public class SubmitQuizAttemptResponse
    {
        public Guid AttemptId { get; set; }

        public double Score { get; set; }

        public bool Passed { get; set; }

        public DateTime SubmittedAt { get; set; }
    }
}
