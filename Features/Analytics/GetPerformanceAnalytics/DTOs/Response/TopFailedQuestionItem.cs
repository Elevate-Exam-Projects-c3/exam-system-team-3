namespace exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs.Response;


public record TopFailedQuestionItem(
    Guid QuestionId, string QuestionText, string QuizTitle,
    int TotalAnswers, int CorrectAnswers, double CorrectRate);