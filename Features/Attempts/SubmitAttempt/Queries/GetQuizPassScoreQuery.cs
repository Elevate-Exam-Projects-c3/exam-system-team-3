namespace exam_system.Features.Attempts.SubmitAttempt.Queries
{
    public record GetQuizPassScoreQuery(Guid QuizId) : IRequest<int?>;
}
