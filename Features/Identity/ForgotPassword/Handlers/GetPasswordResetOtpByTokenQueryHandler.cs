using System.Security.Cryptography;
using System.Text;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.ForgotPassword.DTOs.Internal;
using exam_system.Features.Identity.ForgotPassword.Queries;

namespace exam_system.Features.Identity.ForgotPassword.Handlers;

public class GetPasswordResetOtpByTokenQueryHandler(
    IGenericRepository<PasswordResetOtp> otpRepo)
    : IRequestHandler<GetPasswordResetOtpByTokenQuery, ResetTokenLookupResult?>
{
    public async Task<ResetTokenLookupResult?> Handle(
        GetPasswordResetOtpByTokenQuery request, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLower();
        var resetTokenHash = Convert.ToHexString(
            SHA256.HashData(
                Encoding.UTF8.GetBytes(request.ResetToken)));

        return await otpRepo
            .Get(o => o.Email == normalizedEmail && o.ResetToken == resetTokenHash && o.IsUsed)
            .Select(o => new ResetTokenLookupResult(o.Id, o.UserId, o.ResetTokenExpiresAt))
            .FirstOrDefaultAsync(cancellationToken);
    }
}