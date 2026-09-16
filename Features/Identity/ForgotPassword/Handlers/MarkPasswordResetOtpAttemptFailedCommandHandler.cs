using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.ForgotPassword.Commands;

namespace exam_system.Features.Identity.ForgotPassword.Handlers;

public class MarkPasswordResetOtpAttemptFailedCommandHandler(
    IGenericRepository<PasswordResetOtp> otpRepo,
    IUnitOfWork unitOfWork)
    : IRequestHandler<MarkPasswordResetOtpAttemptFailedCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(
        MarkPasswordResetOtpAttemptFailedCommand request, CancellationToken cancellationToken)
    {
        var otp = await otpRepo.GetByIdAsync(request.OtpId);

        if (otp is not null)
        {
            otp.AttemptCount++;
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return Result.Updated;

    }
}