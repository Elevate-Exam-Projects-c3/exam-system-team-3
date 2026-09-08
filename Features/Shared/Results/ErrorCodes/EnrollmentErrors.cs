using exam_system.Features.Shared.Results;

namespace exam_system.Features.Enrollments.EnrollInDiploma;

public static class EnrollmentErrors
{
    public static readonly Error Unauthorized =
        Error.Unauthorized(
            "Enrollment.Unauthorized",
            "The current user could not be identified.");

    public static readonly Error DiplomaNotAvailable =
        Error.NotFound(
            "Enrollment.DiplomaNotAvailable",
            "The diploma was not found or is not currently available for enrollment.");

    public static readonly Error AlreadyEnrolled =
        Error.Conflict(
            "Enrollment.AlreadyEnrolled",
            "The student is already enrolled in this diploma.");
}