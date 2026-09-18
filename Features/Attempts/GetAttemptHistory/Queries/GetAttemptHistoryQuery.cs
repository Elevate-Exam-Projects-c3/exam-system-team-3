using exam_system.Features.Attempts.GetAttemptHistory.ViewModels;

namespace exam_system.Features.Attempts.GetAttemptHistory.Queries
{
    public sealed record GetAttemptHistoryQuery(Guid StudentId, int PageIndex = 1, int PageSize = 20)
        : IRequest<Result<PaginatedResult<AttemptHistoryItemViewModel>>>;
}
