using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs.Response;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Orchestrators;

public record PerformanceAnalyticsOrchestrator(
    DateTime? DateFrom,
    DateTime? DateTo,
    Guid? DiplomaId
) : IRequest<Result<PerformanceAnalyticsResponse>>;