namespace exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs.Response;

public record AverageScoreByDiplomaItem(
    Guid DiplomaId, string DiplomaTitle, double AverageScore, int TotalAttempts);