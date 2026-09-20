
using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs.Response;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Queries;

public record GetPassRateByQuizQuery(DateTime? DateFrom, DateTime? DateTo, Guid? DiplomaId)
    : IRequest<List<PassRateByQuizItem>>;