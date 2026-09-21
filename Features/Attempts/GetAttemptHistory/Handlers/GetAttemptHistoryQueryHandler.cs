using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.GetAttemptHistory.Queries;
using exam_system.Features.Attempts.GetAttemptHistory.ViewModels;

namespace exam_system.Features.Attempts.GetAttemptHistory.Handlers
{
    public sealed class GetAttemptHistoryQueryHandler : IRequestHandler<GetAttemptHistoryQuery,Result<PaginatedResult<AttemptHistoryItemViewModel>>>
    {
        private readonly IGenericRepository<QuizAttempt> _attemptRepository;

        public GetAttemptHistoryQueryHandler(
            IGenericRepository<QuizAttempt> attemptRepository)
        {
            _attemptRepository = attemptRepository;
        }

        public async Task<Result<PaginatedResult<AttemptHistoryItemViewModel>>> Handle(
            GetAttemptHistoryQuery request,
            CancellationToken cancellationToken)
        {
            var query = _attemptRepository
                .Get(x =>
                    x.StudentId == request.StudentId &&
                    !x.IsDeleted);

            var totalCount = await query.CountAsync(cancellationToken);

            var attempts = await query
                .OrderByDescending(x =>
                    x.SubmittedAt ?? x.StartTime)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new AttemptHistoryItemViewModel
                {
                    AttemptId = x.Id,
                    QuizId = x.QuizId,
                    QuizTitle = x.Quiz.Title,
                    Status = x.Status,
                    Score = x.Score,
                    Passed = x.Passed,
                    StartTime = x.StartTime,
                    SubmittedAt = x.SubmittedAt
                })
                .ToListAsync(cancellationToken);

            var result = new PaginatedResult<AttemptHistoryItemViewModel>(
                attempts,
                totalCount,
                request.PageIndex,
                request.PageSize);

            return Result<PaginatedResult<AttemptHistoryItemViewModel>>.Success(
                result);
        }
    }
}
