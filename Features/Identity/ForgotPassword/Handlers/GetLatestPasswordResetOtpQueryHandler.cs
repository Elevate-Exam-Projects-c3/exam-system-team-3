using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.ForgotPassword.DTOs.Internal;
using exam_system.Features.Identity.ForgotPassword.Queries;

namespace exam_system.Features.Identity.ForgotPassword.Handlers;

public class GetLatestPasswordResetOtpQueryHandler (
    IGenericRepository<PasswordResetOtp> otpRpo
    ):IRequestHandler<GetLatestPasswordResetOtpQuery,PasswordResetOtpCooldownResult?>
{
    public async Task<PasswordResetOtpCooldownResult?> Handle(GetLatestPasswordResetOtpQuery request, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLower();
        
        return await otpRpo
            .Get(o=>o.Email==normalizedEmail)
            .OrderByDescending(o=>o.CreatedAt)
            .Select(o=>new PasswordResetOtpCooldownResult(o.CreatedAt))
            .FirstOrDefaultAsync(cancellationToken);
            
    }
}