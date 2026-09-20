using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs.Response;
using exam_system.Features.Analytics.GetPerformanceAnalytics.Queries;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Handlers;

public class GetAttemptsOverTimeQueryHandler(IGenericRepository<QuizAttempt> attemptRepo)
    : IRequestHandler<GetAttemptsOverTimeQuery, List<AttemptsOverTimeItem>>
{
    public async Task<List<AttemptsOverTimeItem>> Handle(
        GetAttemptsOverTimeQuery request, CancellationToken cancellationToken)
    {
        var grouped = await attemptRepo
            .Get(a => a.SubmittedAt != null
                      && (request.DateFrom == null || a.SubmittedAt >= request.DateFrom)
                      && (request.DateTo == null || a.SubmittedAt <= request.DateTo)
                      && (request.DiplomaId == null || a.Quiz.DiplomaId == request.DiplomaId))
            .GroupBy(a => a.SubmittedAt!.Value.Date)
            .Select(g => new { Date = g.Key, Count = g.Count() })
            .OrderBy(x => x.Date)
            .ToListAsync(cancellationToken);

        return grouped
            .Select(x => new AttemptsOverTimeItem(DateOnly.FromDateTime(x.Date), x.Count))
            .ToList();
    }
}