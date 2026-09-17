
namespace exam_system.Features.Identity.ForgotPassword.Commands;

public record UpdateUserPasswordCommand(Guid UserId, string NewPassword) : IRequest<Result<Updated>>;