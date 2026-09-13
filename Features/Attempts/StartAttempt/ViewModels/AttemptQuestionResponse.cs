namespace exam_system.Features.Attempts.StartAttempt.ViewModels
{
    public class AttemptQuestionResponse
    {
        public Guid Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }

        public List<AttemptOptionResponse> Options { get; set; } = new();
    }
}
