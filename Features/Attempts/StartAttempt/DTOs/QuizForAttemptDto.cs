namespace exam_system.Features.Attempts.StartAttempt.DTOs
{
    public class QuizForAttemptDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public int DurationMinutes { get; set; }

        public int PassScore { get; set; }

        public int? MaxAttempts { get; set; }

        public QuizStatus Status { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<QuestionForAttemptDto> Questions { get; set; } = [];
    }
}
