using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs.Response;
using exam_system.Features.Analytics.GetPerformanceAnalytics.Queries;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Handlers;

public class GetAverageScoreByDiplomaQueryHandler(
    IGenericRepository<QuizAttempt> attemptRepo)
:IRequestHandler<GetAverageScoreByDiplomaQuery,List<AverageScoreByDiplomaItem>>
{
    public async Task<List<AverageScoreByDiplomaItem>> Handle(GetAverageScoreByDiplomaQuery request, CancellationToken cancellationToken)
    {
        var grouped = await attemptRepo
            .Get(a => a.SubmittedAt != null
                      && (request.DateFrom == null || a.SubmittedAt >= request.DateFrom)
                      && (request.DateTo == null || a.SubmittedAt <= request.DateTo)
                      && (request.DiplomaId == null || a.Quiz.DiplomaId == request.DiplomaId))
            .GroupBy(a => new { a.Quiz.DiplomaId, a.Quiz.Diploma.Title })
            .Select(g => new AverageScoreByDiplomaItem(
                g.Key.DiplomaId,
                g.Key.Title,
                g.Average(x => x.Score ?? 0),
                g.Count()))
            .ToListAsync(cancellationToken);

        return grouped;
    }
}