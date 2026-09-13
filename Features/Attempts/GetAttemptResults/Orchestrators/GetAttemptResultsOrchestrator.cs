using exam_system.Features.Attempts.GetAttemptResults.ViewModels;

namespace exam_system.Features.Attempts.GetAttemptResults.Orchestrators
{
    public record GetAttemptResultsOrchestrator(Guid AttemptId, Guid StudentId) : IRequest<Result<GetAttemptResultsResponse>>;
}
