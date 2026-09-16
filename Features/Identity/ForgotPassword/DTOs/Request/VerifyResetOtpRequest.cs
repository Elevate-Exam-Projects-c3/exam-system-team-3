namespace exam_system.Features.Identity.ForgotPassword.DTOs.Request;

public record VerifyResetOtpRequest(string  Email , string Otp);