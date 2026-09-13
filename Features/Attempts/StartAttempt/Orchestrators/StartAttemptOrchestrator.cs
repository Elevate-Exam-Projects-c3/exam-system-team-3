using exam_system.Features.Attempts.StartAttempt.ViewModels;

namespace exam_system.Features.Attempts.StartAttempt.Orchestrators
{
    public record StartAttemptOrchestrator(Guid QuizId,Guid StudentId) : IRequest<Result<StartAttemptResponse>>;
}
