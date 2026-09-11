using exam_system.Features.Identity.VerifyEmailOtp.DTOs.Response;
using exam_system.Features.Shared;

namespace exam_system.Features.Identity.VerifyEmailOtp.Orchestrators;

public record VerifyEmailOtpOrchestrator(
    string Email,
    string Otp
) : IRequest<RequestResponse<VerifyEmailOtpResponse>>;