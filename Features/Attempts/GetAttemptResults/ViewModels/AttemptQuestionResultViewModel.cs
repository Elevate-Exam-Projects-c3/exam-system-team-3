namespace exam_system.Features.Attempts.GetAttemptResults.ViewModels
{
    public class AttemptQuestionResultViewModel 
    {
        public Guid QuestionId { get; set; } 
        public string QuestionText { get; set; } = string.Empty; 
        public Guid? SelectedOptionId { get; set; } 
        public string? SelectedOptionText { get; set; } 
        public bool? IsCorrect { get; set; } 
        public string? Explanation { get; set; } }
}
