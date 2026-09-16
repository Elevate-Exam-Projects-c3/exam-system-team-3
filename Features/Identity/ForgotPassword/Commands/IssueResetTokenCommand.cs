namespace exam_system.Features.Identity.ForgotPassword.Commands;

public record IssueResetTokenCommand(Guid OtpId) : IRequest<Result<string>>;