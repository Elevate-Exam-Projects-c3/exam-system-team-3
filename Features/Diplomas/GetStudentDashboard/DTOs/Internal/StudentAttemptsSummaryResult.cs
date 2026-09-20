using exam_system.Features.Diplomas.GetStudentDashboard.DTOs.Response;

namespace exam_system.Features.Diplomas.GetStudentDashboard.DTOs.Internal;

public record StudentAttemptsSummaryResult(
    IReadOnlyList<RecentAttemptItem> RecentAttempts,
    double AverageScore,
    double PassRate,
    int TotalCount,
    double TotalTimeSpentMinutes
);