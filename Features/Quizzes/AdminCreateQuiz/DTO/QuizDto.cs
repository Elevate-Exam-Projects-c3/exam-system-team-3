using exam_system.Common.Enums;

namespace exam_system.Features.Quizzes.AdminCreateQuiz.DTO
{
    public record QuizDto(
    Guid Id,
    Guid DiplomaId,
    string Title,
    string? Instructions,
    int DurationMinutes,
    int PassScore,
    int? MaxAttempts,
    QuizStatus Status,
    DateTime? PublishedAt
    );
}
