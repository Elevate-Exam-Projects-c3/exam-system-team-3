using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs.Response;
using exam_system.Features.Analytics.GetPerformanceAnalytics.Orchestrators;
using exam_system.Features.Analytics.GetPerformanceAnalytics.Queries;
using Microsoft.Extensions.Caching.Memory;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Handlers;

public class PerformanceAnalyticsOrchestratorHandler(
    IMediator mediator,
    IMemoryCache cache)
    : IRequestHandler<PerformanceAnalyticsOrchestrator, Result<PerformanceAnalyticsResponse>>
{
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);

    public async Task<Result<PerformanceAnalyticsResponse>> Handle(
        PerformanceAnalyticsOrchestrator request,
        CancellationToken cancellationToken)
    {
        var cacheKey = BuildCacheKey(request);

        if (cache.TryGetValue(cacheKey, out PerformanceAnalyticsResponse? cachedResponse))
        {
            return Result<PerformanceAnalyticsResponse>.Success(cachedResponse!);
        }

        var passRateByQuiz = await mediator.Send(
            new GetPassRateByQuizQuery(request.DateFrom, request.DateTo, request.DiplomaId),
            cancellationToken);

        var averageScoreByDiploma = await mediator.Send(
            new GetAverageScoreByDiplomaQuery(request.DateFrom, request.DateTo, request.DiplomaId),
            cancellationToken);

        var attemptsOverTime = await mediator.Send(
            new GetAttemptsOverTimeQuery(request.DateFrom, request.DateTo, request.DiplomaId),
            cancellationToken);

        var topFailedQuestions = await mediator.Send(
            new GetTopFailedQuestionsQuery(request.DateFrom, request.DateTo, request.DiplomaId),
            cancellationToken);

        var response = new PerformanceAnalyticsResponse(
            passRateByQuiz,
            averageScoreByDiploma,
            attemptsOverTime,
            topFailedQuestions);

        cache.Set(cacheKey, response, CacheDuration);

        return Result<PerformanceAnalyticsResponse>.Success(response);
    }

    private static string BuildCacheKey(PerformanceAnalyticsOrchestrator request)
        => $"analytics:performance:{request.DateFrom:yyyyMMdd}:{request.DateTo:yyyyMMdd}:{request.DiplomaId}";
}