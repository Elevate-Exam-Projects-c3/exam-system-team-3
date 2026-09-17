namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.DTOs;

public sealed record PublishCheckData(
    int QuestionsCount,
    IReadOnlyList<int> CorrectOptionsCountPerQuestion,
    int DurationMinutes,
    int PassScore);