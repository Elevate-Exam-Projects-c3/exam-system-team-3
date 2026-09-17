using exam_system.Features.Identity.ForgotPassword.DTOs.Response;
using exam_system.Features.Shared.Results;

namespace exam_system.Features.Identity.ForgotPassword.Orchestrators;

public record ResetPasswordOrchestrator(
    string Email, string ResetToken, string NewPassword, string ConfirmPassword
) : IRequest<Result<ResetPasswordResponse>>;