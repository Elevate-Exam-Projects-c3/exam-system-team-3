using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.VerifyEmailOtp.Commands;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Identity.VerifyEmailOtp.Handlers;

public class MarkOtpUsedCommandHandler(
    IGenericRepository<EmailVerificationOtp> otpRepo)
    : IRequestHandler<MarkOtpUsedCommand>
{
    public async Task Handle(MarkOtpUsedCommand request, CancellationToken cancellationToken)
    {
        var otp = await otpRepo
            .Get(o => o.Id == request.OtpId)
            .FirstOrDefaultAsync(cancellationToken);

        if (otp is not null)
            otp.IsUsed = true;
    }
}