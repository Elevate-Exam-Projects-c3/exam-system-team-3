using System.Security.Cryptography;
using System.Text;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.ForgotPassword.Commands;
using exam_system.Features.Shared.Results.ErrorCodes;

namespace exam_system.Features.Identity.ForgotPassword.Handlers;

public class IssueResetTokenCommandHandler(
    IGenericRepository<PasswordResetOtp> otpRepo,
    IUnitOfWork unitOfWork)
    : IRequestHandler<IssueResetTokenCommand, Result<string>>
{
    public async Task<Result<string>> Handle(
        IssueResetTokenCommand request, CancellationToken cancellationToken)
    {
        var otp = await otpRepo.GetByIdAsync(request.OtpId);

        if (otp is null)
        {
            return VerifyResetOtpErrors.OtpNotFound;
        }

        var resetToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(48));
        var resetTokenHash = Convert.ToHexString(
            SHA256.HashData(
                Encoding.UTF8.GetBytes(resetToken)));

        otp.ResetToken = resetTokenHash;
        otp.ResetTokenExpiresAt = DateTime.UtcNow.AddMinutes(10);
        otp.IsUsed = true;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Success(resetToken);
    }
}