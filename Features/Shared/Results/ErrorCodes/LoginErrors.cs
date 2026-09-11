namespace exam_system.Features.Shared.Results.ErrorCodes;

public static class LoginErrors
{
    public static readonly Error InvalidCredentials =
        Error.Unauthorized("LOGIN_INVALID_CREDENTIALS", "Invalid email or password.");

    public static readonly Error AccountNotVerified =
        Error.Validation("LOGIN_ACCOUNT_NOT_VERIFIED", "Please verify your email before logging in.");

    public static readonly Error AccountLocked =
        Error.Forbidden("LOGIN_ACCOUNT_LOCKED", "Account locked due to multiple failed attempts. Try again later.");
}