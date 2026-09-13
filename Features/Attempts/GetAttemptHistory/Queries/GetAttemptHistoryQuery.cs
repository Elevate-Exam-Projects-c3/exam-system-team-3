using exam_system.Features.Attempts.GetAttemptHistory.ViewModels;

namespace exam_system.Features.Attempts.GetAttemptHistory.Queries
{
    public record GetAttemptHistoryQuery(Guid StudentId) : IRequest<GetAttemptHistoryResponse>;
}
