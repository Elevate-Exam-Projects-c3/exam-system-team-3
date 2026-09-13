namespace exam_system.Features.Attempts.StartAttempt.DTOs
{
    public class QuestionForAttemptDto
    {
        public Guid Id { get; set; }

        public string QuestionText { get; set; } = null!;

        public List<OptionForAttemptDto> Options { get; set; } = [];
    }
}
