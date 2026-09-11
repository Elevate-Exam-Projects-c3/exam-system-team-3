namespace exam_system.Features.Identity.VerifyEmailOtp.Commands;

public record MarkOtpAttemptFailedCommand(Guid OtpId) : IRequest;