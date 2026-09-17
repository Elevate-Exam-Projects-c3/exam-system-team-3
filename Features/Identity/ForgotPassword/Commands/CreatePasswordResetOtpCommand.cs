namespace exam_system.Features.Identity.ForgotPassword.Commands;

public record CreatePasswordResetOtpCommand(
    Guid  UserId,
    string Email):IRequest<Result<string>>;