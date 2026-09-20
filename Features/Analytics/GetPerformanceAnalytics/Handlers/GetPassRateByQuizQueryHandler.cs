using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs.Response;
using exam_system.Features.Analytics.GetPerformanceAnalytics.Queries;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Handlers;

public class GetPassRateByQuizQueryHandler(
    IGenericRepository<QuizAttempt> attemptRepo)
:IRequestHandler<GetPassRateByQuizQuery,List<PassRateByQuizItem>>
{
    public async Task<List<PassRateByQuizItem>> Handle(GetPassRateByQuizQuery request,
        CancellationToken cancellationToken)
    {
        var grouped = await attemptRepo
            .Get(a => a.SubmittedAt != null
                      && (request.DateFrom == null || a.SubmittedAt >= request.DateFrom)
                      && (request.DateTo == null || a.SubmittedAt <= request.DateTo)
                      && (request.DiplomaId == null || a.Quiz.DiplomaId == request.DiplomaId))
            .GroupBy(a => new { a.QuizId, a.Quiz.Title })
            .Select(g => new
            {
                g.Key.QuizId,
                g.Key.Title,
                Total = g.Count(),
                Passed = g.Count(x => x.Passed == true)
            })
            .ToListAsync(cancellationToken);
        
        return grouped
            .Select(x => new PassRateByQuizItem(
                x.QuizId, x.Title, x.Total, x.Passed,
                x.Total == 0 ? 0 : Math.Round((double)x.Passed / x.Total * 100, 2)))
            .ToList();
    }
}