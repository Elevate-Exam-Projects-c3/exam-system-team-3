namespace exam_system.Features.Attempts.SubmitAttempt.Queries
{
    public record GetQuizQuestionCountQuery(Guid QuizId) : IRequest<int>;
}
