namespace exam_system.Features.Attempts.StartAttempt.ViewModels
{
    public class StartAttemptResponse
    {
        public Guid AttemptId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime Deadline { get; set; }

        public List<AttemptQuestionResponse> Questions { get; set; }  = new();
    }
}
