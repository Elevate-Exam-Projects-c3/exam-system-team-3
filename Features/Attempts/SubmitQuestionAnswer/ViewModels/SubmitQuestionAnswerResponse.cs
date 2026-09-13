namespace exam_system.Features.Attempts.SubmitQuestionAnswer.ViewModels
{
    public class SubmitQuestionAnswerResponse
    {
        public Guid AnswerId { get; set; }

        public Guid QuestionId { get; set; }

        public Guid SelectedOptionId { get; set; }

        public DateTime AnsweredAt { get; set; }
    }
}
