namespace exam_system.Features.Quizzes.AdminUpdateQuiz.DTO
{
    public sealed class UpdateQuizRequest
    {
        public string Title { get; set; } = string.Empty;

        public string? Instructions { get; set; }

        public int DurationMinutes { get; set; }

        public int PassScore { get; set; }

        public int? MaxAttempts { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
