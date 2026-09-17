using exam_system.Features.Identity.ForgotPassword.DTOs.Response;

namespace exam_system.Features.Identity.ForgotPassword.Orchestrators;

public record ForgotPasswordOrchestrator(
    string Email) : IRequest<Result<ForgotPasswordResponse>>;