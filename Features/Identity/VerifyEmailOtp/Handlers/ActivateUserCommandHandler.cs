using exam_system.Common.Enums;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.VerifyEmailOtp.Commands;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Identity.VerifyEmailOtp.Handlers;

public class ActivateUserCommandHandler(
    IGenericRepository<ApplicationUser> userRepo)
    : IRequestHandler<ActivateUserCommand>
{
    public async Task Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepo
            .Get(u => u.Id == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is not null)
        {
            user.AccountStatus = AccountStatus.Active;
            user.EmailConfirmed = true;
        }
    }
}