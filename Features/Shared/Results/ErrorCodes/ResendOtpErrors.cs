namespace exam_system.Features.Shared.Results.ErrorCodes;

public static class ResendOtpErrors
{
    public static readonly Error UserNotFound =
        Error.NotFound("RESEND_OTP_USER_NOT_FOUND", "No account found for this email.");

    public static readonly Error AlreadyVerified =
        Error.Validation("RESEND_OTP_ALREADY_VERIFIED", "This account is already verified. Please log in.");
}