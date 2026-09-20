using exam_system.Domain.Entities.Attempts;

namespace exam_system.Features.Attempts.SubmitAttempt.Queries
{
    public record GetAttemptForSubmitQuery(Guid AttemptId,Guid StudentId) : IRequest<QuizAttempt?>;
}
