using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.ResendOtp.Commands;

namespace exam_system.Features.Identity.ResendOtp.Handlers;

public class InvalidateActiveOtpsCommandHandler(
    IGenericRepository<EmailVerificationOtp> otpRepo,
    IUnitOfWork unitOfWork)
    :IRequestHandler<InvalidateActiveOtpsCommand>
{
    public async Task Handle(InvalidateActiveOtpsCommand request, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLower();

        var activOtps = await otpRepo
            .Get(o => o.Email == normalizedEmail && !o.IsUsed)
            .ToListAsync(cancellationToken);

        foreach (var otp in  activOtps  )
        {
            otp.IsUsed = true;

        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

    }
}