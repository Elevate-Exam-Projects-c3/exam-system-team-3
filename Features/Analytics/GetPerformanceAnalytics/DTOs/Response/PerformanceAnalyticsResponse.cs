namespace exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs.Response;

public record PerformanceAnalyticsResponse(
    IReadOnlyList<PassRateByQuizItem> PassRateByQuiz,
    IReadOnlyList<AverageScoreByDiplomaItem> AverageScoreByDiploma,
    IReadOnlyList<AttemptsOverTimeItem> AttemptsOverTime,
    IReadOnlyList<TopFailedQuestionItem> TopFailedQuestions
);