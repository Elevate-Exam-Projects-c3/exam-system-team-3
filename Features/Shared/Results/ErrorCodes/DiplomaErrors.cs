namespace exam_system.Features.Shared.Results.ErrorCodes;

public class DiplomaErrors
{
    public static readonly Error NotFound =
    Error.NotFound(
        "Diploma_NotFound",
        "Diploma was not found.");

    public static readonly Error HasActiveEnrollments =
        Error.Conflict(
            "Diploma_HasActiveEnrollments",
            "Diploma cannot be deleted while it has active enrollments.");
}
