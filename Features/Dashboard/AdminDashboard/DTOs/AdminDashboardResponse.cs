namespace exam_system.Features.Dashboard.AdminDashboard.DTOs;

public sealed record AdminDashboardResponse(
    int TotalRegisteredUsers,
    int ActiveUsersToday,
    int TotalDiplomas,
    int TotalQuizzes,
    int TotalAttempts,
    double OverallAveragePassRate);