namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Queries;

using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs.Response;


public record GetTopFailedQuestionsQuery(DateTime? DateFrom, DateTime? DateTo, Guid? DiplomaId)
    : IRequest<List<TopFailedQuestionItem>>;