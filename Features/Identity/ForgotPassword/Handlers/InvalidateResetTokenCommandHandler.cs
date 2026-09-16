using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.ForgotPassword.Commands;

namespace exam_system.Features.Identity.ForgotPassword.Handlers;

public class InvalidateResetTokenCommandHandler(
    IGenericRepository<PasswordResetOtp> otpRepo,
    IUnitOfWork unitOfWork)
    : IRequestHandler<InvalidateResetTokenCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(
        InvalidateResetTokenCommand request, CancellationToken cancellationToken)
    {
        var otp = await otpRepo.GetByIdAsync(request.OtpId);

        if (otp is not null)
        {
            otp.ResetToken = null;
            otp.ResetTokenExpiresAt = null;
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return Result.Updated;

    }
}