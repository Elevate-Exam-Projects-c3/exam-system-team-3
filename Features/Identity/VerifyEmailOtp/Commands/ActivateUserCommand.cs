namespace exam_system.Features.Identity.VerifyEmailOtp.Commands;

public record ActivateUserCommand(Guid UserId) : IRequest;