using exam_system.Domain.Entities.Attempts;

namespace exam_system.Features.Attempts.StartAttempt.Queries
{
    public record GetInProgressAttemptQuery(Guid StudentId,Guid QuizId) : IRequest<QuizAttempt?>;
}
