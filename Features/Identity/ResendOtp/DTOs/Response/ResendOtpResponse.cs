namespace exam_system.Features.Identity.ResendOtp.DTOs.Response;

public record ResendOtpResponse(
    string  Email,
    string Massage);