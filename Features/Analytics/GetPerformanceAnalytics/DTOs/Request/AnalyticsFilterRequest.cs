namespace exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs.Request;

public record AnalyticsFilterRequest(DateTime? DateFrom, DateTime? DateTo, Guid? DiplomaId);