using System.Security.Cryptography;
using System.Text;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.Register.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Identity.Register.Handlers;

public class CreateEmailOtpCommandHandler(
    IGenericRepository<EmailVerificationOtp> otpRepo) 
    : IRequestHandler<CreateEmailOtpCommand, RequestResponse<string>>
{
    public async Task<RequestResponse<string>> Handle(CreateEmailOtpCommand request, CancellationToken cancellationToken)
    {
        var plainOtp = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();

        var otpHash = Convert.ToHexString(
            SHA256.HashData(Encoding.UTF8.GetBytes(plainOtp))
        );

        var otp = new EmailVerificationOtp
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Email = request.Email,
            OtpHash = otpHash,
            ExpiresAt = DateTime.UtcNow.AddMinutes(10),
            AttemptCount = 0,
            IsUsed = false
        };

        await otpRepo.AddAsync(otp);

        return RequestResponse<string>.Ok(plainOtp);
    }
}