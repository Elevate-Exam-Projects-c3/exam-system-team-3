using exam_system.Features.Shared.Results;

namespace exam_system.Features.Identity.ForgotPassword.Commands;

public record InvalidateResetTokenCommand(Guid OtpId) : IRequest<Result<Updated>>;