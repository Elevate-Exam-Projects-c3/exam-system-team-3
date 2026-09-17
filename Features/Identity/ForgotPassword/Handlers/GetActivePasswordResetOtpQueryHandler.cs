using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.ForgotPassword.DTOs.Internal;
using exam_system.Features.Identity.ForgotPassword.Queries;

namespace exam_system.Features.Identity.ForgotPassword.Handlers;

public class GetActivePasswordResetOtpQueryHandler(
    IGenericRepository<PasswordResetOtp> otpRepo)
    : IRequestHandler<GetActivePasswordResetOtpQuery, PasswordResetOtpLookupResult?>
{
    public async Task<PasswordResetOtpLookupResult?> Handle(
        GetActivePasswordResetOtpQuery request, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLower();

        return await otpRepo
            .Get(o => o.Email == normalizedEmail && !o.IsUsed)
            .OrderByDescending(o => o.CreatedAt)
            .Select(o => new PasswordResetOtpLookupResult(o.Id, o.UserId, o.OtpHash, o.AttemptCount, o.ExpiresAt))
            .FirstOrDefaultAsync(cancellationToken);
    }
}