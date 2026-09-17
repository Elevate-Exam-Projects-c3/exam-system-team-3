namespace exam_system.Features.Shared.Results.ErrorCodes;

public class QuizErrors
{
    public static readonly Error NotFound = Error.NotFound(
    "Quiz.NotFound",
    "Quiz was not found.");
}
