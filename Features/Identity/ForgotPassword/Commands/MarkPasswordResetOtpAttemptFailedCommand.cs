
namespace exam_system.Features.Identity.ForgotPassword.Commands;

public record MarkPasswordResetOtpAttemptFailedCommand(Guid OtpId) : IRequest<Result<Updated>>;