using exam_system.Features.Analytics.GetPerformanceAnalytics.Orchestrators;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Validators;

public class PerformanceAnalyticsOrchestratorValidator
    : AbstractValidator<PerformanceAnalyticsOrchestrator>
{
    public PerformanceAnalyticsOrchestratorValidator()
    {
        RuleFor(x => x)
            .Must(x => x.DateFrom is null || x.DateTo is null || x.DateFrom <= x.DateTo)
            .WithMessage("'dateFrom' must be earlier than or equal to 'dateTo'.");
    }
}