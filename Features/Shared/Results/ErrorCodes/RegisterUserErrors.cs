namespace exam_system.Features.Shared.Results.ErrorCodes;

public static class RegisterUserErrors
{
    public static readonly Error EmailAlreadyRegistered =
        Error.Conflict("Register_Email_AlreadyRegistered", "Email is already registered.");
}