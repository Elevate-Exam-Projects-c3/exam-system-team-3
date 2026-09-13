namespace exam_system.Features.Attempts.StartAttempt.Queries
{
    public record GetAttemptCountQuery(Guid StudentId,Guid QuizId) : IRequest<int>;
}
