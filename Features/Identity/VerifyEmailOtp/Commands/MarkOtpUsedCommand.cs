namespace exam_system.Features.Identity.VerifyEmailOtp.Commands;

public record MarkOtpUsedCommand(Guid OtpId) : IRequest;