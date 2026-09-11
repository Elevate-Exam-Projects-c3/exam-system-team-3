using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.VerifyEmailOtp.DTOs.Internal;
using exam_system.Features.Identity.VerifyEmailOtp.Queries;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Identity.VerifyEmailOtp.Handlers;

public class GetActiveOtpQueryHandler(
    IGenericRepository<EmailVerificationOtp> otpRepo)
    : IRequestHandler<GetActiveOtpQuery, OtpLookupResult?>
{
    public async Task<OtpLookupResult?> Handle(
        GetActiveOtpQuery request,
        CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLower();

        return await otpRepo
            .Get(o => o.Email == normalizedEmail && !o.IsUsed)
            .OrderByDescending(o => o.CreatedAt)
            .Select(o => new OtpLookupResult(o.Id, o.OtpHash, o.AttemptCount, o.ExpiresAt))
            .FirstOrDefaultAsync(cancellationToken);
    }
}