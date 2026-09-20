namespace exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs.Response;

public record PassRateByQuizItem(
    Guid QuizId, string QuizTitle, int TotalAttempts, int PassedAttempts, double PassRate);