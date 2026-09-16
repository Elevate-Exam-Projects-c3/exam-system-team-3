using exam_system.Features.Identity.ResendOtp.DTOs.Response;

namespace exam_system.Features.Identity.ResendOtp.Orchestrators;

public record ResendOtpOrchestrator(
    string  Email):IRequest<Result<ResendOtpResponse>>;