namespace exam_system.Features.Diplomas.GetStudentDashboard.DTOs.Response;

public record RecentAttemptItem(
    Guid Id,
    Guid QuizId,
    string QuizTitle,
    double? Score,
    bool? Passed,
    DateTime? SubmittedAt
);