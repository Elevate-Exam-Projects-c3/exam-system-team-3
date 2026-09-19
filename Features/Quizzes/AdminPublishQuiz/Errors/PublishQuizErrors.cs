namespace exam_system.Features.Shared.Results.ErrorCodes;

public static class PublishQuizErrors
{
    public static readonly Error HasNoQuestions =
        Error.Validation("Quiz_Has_No_Questions","Quiz must contain at least one question.");

    public static readonly Error QuestionsMustHaveExactlyOneCorrectOption =
        Error.Validation("Quiz_Questions_Invalid_Correct_Options","Every question must have exactly one correct option.");

    public static readonly Error InvalidDuration =
        Error.Validation("Quiz_Duration_Invalid","Quiz duration must be greater than zero.");

    public static readonly Error InvalidPassScore =
        Error.Validation("Quiz_PassScore_Invalid","Quiz pass score must be between 0 and 100.");
}