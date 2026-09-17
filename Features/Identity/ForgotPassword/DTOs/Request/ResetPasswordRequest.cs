namespace exam_system.Features.Identity.ForgotPassword.DTOs.Request;

public record ResetPasswordRequest(
    string Email,
    string ResetToken,
    string NewPassword,
    string ConfirmPassword
);