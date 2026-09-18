using exam_system.Features.Attempts.SubmitAttempt.ViewModels;

namespace exam_system.Features.Attempts.SubmitAttempt.Orchestrators
{
    public record SubmitQuizAttemptOrchestrator(Guid AttemptId,Guid StudentId) : IRequest<Result<SubmitQuizAttemptResponse>>;
}
