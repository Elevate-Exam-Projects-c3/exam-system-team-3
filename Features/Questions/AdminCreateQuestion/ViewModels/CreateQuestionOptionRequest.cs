namespace exam_system.Features.Questions.AdminCreateQuestion.ViewModels
{
    public sealed class CreateQuestionOptionRequest
    {
        public string OptionText { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
