using System.Security.Cryptography;
using System.Text;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.ForgotPassword.Commands;

namespace exam_system.Features.Identity.ForgotPassword.Handlers;

public class CreatePasswordResetOtpCommandHandler(
    IGenericRepository<PasswordResetOtp>  otpRpo
    ):IRequestHandler<CreatePasswordResetOtpCommand,Result<string>>
{
    public async Task<Result<string>> Handle(CreatePasswordResetOtpCommand request, CancellationToken cancellationToken)
    {
        var plainOtp = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();
        
        var otpHash = Convert.ToHexString(
            SHA256.HashData(Encoding.UTF8.GetBytes(plainOtp)));

        var otp = new PasswordResetOtp
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Email = request.Email,
            OtpHash = otpHash,
            ExpiresAt = DateTime.UtcNow.AddMinutes(10),
            AttemptCount = 0,
            IsUsed = false
        };

        await otpRpo.AddAsync(otp);

        return Result<string>.Success(plainOtp);
    }
}