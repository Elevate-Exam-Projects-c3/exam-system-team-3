namespace exam_system.Features.Shared.Results.ErrorCodes;

public static class UnpublishQuizErrors
{
    public static readonly Error HasInProgressAttempts =
        Error.Conflict(
            "Quiz_Has_In_Progress_Attempts",
            "Quiz cannot be unpublished while students have in-progress attempts.");
    public static readonly Error NotFound =
    Error.NotFound(
        "Quiz_Not_Found",
        "Quiz was not found.");
}