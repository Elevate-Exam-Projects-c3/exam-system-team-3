namespace exam_system.Features.Shared.Results.ErrorCodes;


public static class VerifyEmailOtpErrors
{
    public static readonly Error UserNotFound =
        Error.NotFound("VERIFY_OTP_USER_NOT_FOUND", "No account found for this email.");

    public static readonly Error OtpNotFound =
        Error.Validation("VERIFY_OTP_CODE_NOT_FOUND", "No active verification code found. Please request a new one.");

    public static readonly Error OtpExpired =
        Error.Validation("VERIFY_OTP_CODE_EXPIRED", "Code expired. Please request a new one.");

    public static readonly Error OtpLocked =
        Error.Validation("VERIFY_OTP_CODE_LOCKED", "This code has been locked due to too many attempts. Please request a new one.");
    public static readonly Error OtpInvalid =
        Error.Validation("VERIFY_OTP_CODE_INVALID", "Invalid code.");
}