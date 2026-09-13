using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.GetAttemptResults.ViewModels;

namespace exam_system.Features.Attempts.GetAttemptResults.Queries
{
    public record GetAttemptForResultsQuery(Guid AttemptId, Guid StudentId) : IRequest<AttemptResultAttemptViewModel?>;
}
