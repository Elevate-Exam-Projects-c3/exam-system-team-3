namespace exam_system.Features.Attempts.CheckRemainingTime.DTOs
{
    public class RemainingTimeResponse
    {
        public Guid AttemptId { get; set; }

        public DateTime Deadline { get; set; }

        public int RemainingSeconds { get; set; }

        public bool IsExpired { get; set; }
    }
}
