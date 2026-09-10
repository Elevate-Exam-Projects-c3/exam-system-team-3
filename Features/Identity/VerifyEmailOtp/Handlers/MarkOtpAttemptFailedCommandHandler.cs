using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.VerifyEmailOtp.Commands;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Identity.VerifyEmailOtp.Handlers;

public class MarkOtpAttemptFailedCommandHandler(
    IGenericRepository<EmailVerificationOtp> otpRepo)
    : IRequestHandler<MarkOtpAttemptFailedCommand>
{
    public async Task Handle(MarkOtpAttemptFailedCommand request, CancellationToken cancellationToken)
    {
        var otp = await otpRepo
            .Get(o => o.Id == request.OtpId)
            .FirstOrDefaultAsync(cancellationToken);

        if (otp is not null)
            otp.AttemptCount++;
    }
}