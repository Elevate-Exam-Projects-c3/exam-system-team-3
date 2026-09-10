using exam_system.Features.Shared.Results;

public static class RegisterUserErrors
{
    public static readonly Error EmailAlreadyRegistered =
        Error.Conflict("REGISTER_EMAIL_ALREADY_REGISTERED", "Email already registered.");
}