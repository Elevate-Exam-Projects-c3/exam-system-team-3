namespace exam_system.Features.Questions.AdminUpdateQuestion.ViewModels
{
    public sealed class UpdateQuestionOptionRequest
    {
        public Guid? Id { get; set; }

        public string OptionText { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }
    }
}
