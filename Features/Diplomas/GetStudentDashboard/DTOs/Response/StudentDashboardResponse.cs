namespace exam_system.Features.Diplomas.GetStudentDashboard.DTOs.Response;

public record StudentDashboardResponse(
    IReadOnlyList<EnrolledDiplomaItem> EnrolledDiplomas,
    IReadOnlyList<RecentAttemptItem> RecentAttempts,
    double AverageScore,
    double PassRate,
    int TotalAttemptsCount,
    double TotalTimeSpentMinutes  
);