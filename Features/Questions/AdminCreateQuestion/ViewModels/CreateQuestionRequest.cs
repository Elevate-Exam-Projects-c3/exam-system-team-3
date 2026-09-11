namespace exam_system.Features.Questions.AdminCreateQuestion.ViewModels
{
    public sealed class CreateQuestionRequest
    {
        public string Text { get; set; } = string.Empty;

        public string? Explanation { get; set; }

        public int OrderIndex { get; set; }

        public List<CreateQuestionOptionRequest> Options { get; set; } = new();
    }
}
