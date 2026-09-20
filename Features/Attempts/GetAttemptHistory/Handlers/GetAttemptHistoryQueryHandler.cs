using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.GetAttemptHistory.Queries;
using exam_system.Features.Attempts.GetAttemptHistory.ViewModels;

namespace exam_system.Features.Attempts.GetAttemptHistory.Handlers
{
    public class GetAttemptHistoryQueryHandler : IRequestHandler<GetAttemptHistoryQuery, Result<PaginatedResult<AttemptHistoryItemViewModel>>>
    {
        private readonly IGenericRepository<QuizAttempt> _attemptRepository;

        public GetAttemptHistoryQueryHandler(
            IGenericRepository<QuizAttempt> attemptRepository)
        {
            _attemptRepository = attemptRepository;
        }

        public async Task<Result<PaginatedResult<AttemptHistoryItemViewModel>>> Handle(GetAttemptHistoryQuery request,CancellationToken cancellationToken)
        {
            var query = _attemptRepository
                .Get(x =>
                    x.StudentId == request.StudentId &&
                    !x.IsDeleted)
                .OrderByDescending(x => x.StartTime);

            var totalCount = await query.CountAsync(cancellationToken);

            var attempts = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new AttemptHistoryItemViewModel
                {
                    AttemptId = x.Id,
                    QuizId = x.QuizId,
                    QuizTitle = x.Quiz.Title,
                    Status = x.Status,
                    StartTime = x.StartTime,
                    Deadline = x.Deadline,
                    SubmittedAt = x.SubmittedAt,
                    Score = x.Score,
                    Passed = x.Passed
                })
                .ToListAsync(cancellationToken);

            return Result<PaginatedResult<AttemptHistoryItemViewModel>>.Success
                (new PaginatedResult<AttemptHistoryItemViewModel>(attempts,
                totalCount,
                request.PageIndex,
                request.PageSize));
               
        }
    }
}
