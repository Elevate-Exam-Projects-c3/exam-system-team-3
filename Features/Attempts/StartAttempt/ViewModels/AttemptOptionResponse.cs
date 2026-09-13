namespace exam_system.Features.Attempts.StartAttempt.ViewModels
{
    public class AttemptOptionResponse
    {
        public Guid Id { get; set; }

        public string OptionText { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }
    }
}
