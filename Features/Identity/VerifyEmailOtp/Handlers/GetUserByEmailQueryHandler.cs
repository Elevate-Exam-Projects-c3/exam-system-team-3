using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.VerifyEmailOtp.DTOs.Internal;
using exam_system.Features.Identity.VerifyEmailOtp.Queries;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Identity.VerifyEmailOtp.Handlers;

public class GetUserByEmailQueryHandler(
    IGenericRepository<ApplicationUser> userRepo)
    : IRequestHandler<GetUserByEmailQuery, UserLookupResult?>
{
    public async Task<UserLookupResult?> Handle(
        GetUserByEmailQuery request,
        CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLower();

        return await userRepo
            .Get(u => u.Email == normalizedEmail)
            .Select(u => new UserLookupResult(u.Id, u.Email, u.EmailConfirmed))
            .FirstOrDefaultAsync(cancellationToken);
    }
}