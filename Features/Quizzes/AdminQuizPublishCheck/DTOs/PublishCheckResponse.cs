namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.DTOs;

public sealed record PublishCheckResponse(
    bool IsReady,
    bool HasQuestions,
    bool QuestionsHaveExactlyOneCorrectOption,
    bool DurationIsValid,
    bool PassScoreIsValid);