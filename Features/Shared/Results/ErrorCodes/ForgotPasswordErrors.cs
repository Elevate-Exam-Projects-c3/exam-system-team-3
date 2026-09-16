namespace exam_system.Features.Shared.Results.ErrorCodes;

public static class ForgotPasswordErrors
{
    public static readonly Error ResendTooSoon =
        Error.TooManyRequests("FORGOT_PASSWORD_RESEND_TOO_SOON", "Please wait before requesting another code.");
}
public static class VerifyResetOtpErrors
{
    public static readonly Error OtpNotFound =
        Error.Validation("VERIFY_RESET_OTP_NOT_FOUND", "No active reset code found. Please request a new one.");

    public static readonly Error OtpExpired =
        Error.Validation("VERIFY_RESET_OTP_EXPIRED", "Code expired. Please request a new one.");

    public static readonly Error OtpLocked =
        Error.Validation("VERIFY_RESET_OTP_LOCKED", "This code has been locked due to too many attempts. Please request a new one.");

    public static readonly Error OtpInvalid =
        Error.Validation("VERIFY_RESET_OTP_INVALID", "Invalid code.");
}


public static class ResetPasswordErrors
{
    public static readonly Error InvalidResetToken =
        Error.Unauthorized("RESET_PASSWORD_INVALID_TOKEN", "Invalid or expired reset session. Please start over.");

    public static readonly Error PasswordMismatch =
        Error.Validation("RESET_PASSWORD_MISMATCH", "Passwords do not match.");
}