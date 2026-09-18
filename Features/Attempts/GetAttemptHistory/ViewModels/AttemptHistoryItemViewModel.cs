namespace exam_system.Features.Attempts.GetAttemptHistory.ViewModels
{
    public class AttemptHistoryItemViewModel 
    { 
        public Guid AttemptId { get; set; } 
        public Guid QuizId { get; set; } 
        public string QuizTitle { get; set; } = string.Empty; 
        public AttemptStatus Status { get; set; } 
        public DateTime StartTime { get; set; } 
        public DateTime Deadline { get; set; } 
        public DateTime? SubmittedAt { get; set; } 
        public double? Score { get; set; } 
        public bool? Passed { get; set; } 
    }
}
