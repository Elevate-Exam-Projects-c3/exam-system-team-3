namespace exam_system.Features.Shared.Results.ErrorCodes;

public static class PerformanceAnalyticsErrors
{
    public static readonly Error InvalidDateRange =
        Error.Validation("ANALYTICS_INVALID_DATE_RANGE", "The 'dateFrom' value must be earlier than or equal to 'dateTo'.");
}