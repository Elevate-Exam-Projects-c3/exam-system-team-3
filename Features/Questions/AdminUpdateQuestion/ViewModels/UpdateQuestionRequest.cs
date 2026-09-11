namespace exam_system.Features.Questions.AdminUpdateQuestion.ViewModels
{
    public sealed class UpdateQuestionRequest
    {
        public string Text { get; set; } = string.Empty;

        public string? Explanation { get; set; }

        public int OrderIndex { get; set; }

        public List<UpdateQuestionOptionRequest> Options { get; set; } = new();
    }
}
