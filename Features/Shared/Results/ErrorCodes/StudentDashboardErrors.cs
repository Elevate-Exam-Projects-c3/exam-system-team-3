namespace exam_system.Features.Shared.Results.ErrorCodes;

public static class StudentDashboardErrors
{
    public static readonly Error StudentNotFound =
        Error.NotFound("STUDENT_DASHBOARD_NOT_FOUND", "Student profile not found for this account.");
}