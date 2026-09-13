using exam_system.Domain.Entities.Quizzes;

namespace exam_system.Features.Attempts.StartAttempt.Commands
{
    public record StartAttemptCommand(Guid StudentId,Guid QuizId,int DurationMinutes) : IRequest<Result<Guid>>;
}
