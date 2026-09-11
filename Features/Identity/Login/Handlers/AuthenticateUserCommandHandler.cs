using exam_system.Common.Enums;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.Login.Commands;
using exam_system.Features.Identity.Login.DTOs.Internal;
using exam_system.Features.Shared;
using exam_system.Features.Shared.Results.ErrorCodes;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Identity.Login.Handlers;

public class AuthenticateUserCommandHandler(
    IGenericRepository<ApplicationUser> userRepo,
    IUnitOfWork unitOfWork)
    : IRequestHandler<AuthenticateUserCommand, RequestResponse<AuthenticatedUserResult>>
{
    public async Task<RequestResponse<AuthenticatedUserResult>> Handle(
        AuthenticateUserCommand request,
        CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLower();

        var user = await userRepo
            .Get(u => u.Email.ToLower() == normalizedEmail)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return RequestResponse<AuthenticatedUserResult>.Fail(LoginErrors.InvalidCredentials);
        }

        if (user.LockoutEnd is not null && user.LockoutEnd > DateTime.UtcNow)
        {
            return RequestResponse<AuthenticatedUserResult>.Fail(LoginErrors.AccountLocked);
        }

        if (user.AccountStatus != AccountStatus.Active || !user.EmailConfirmed)
        {
            return RequestResponse<AuthenticatedUserResult>.Fail(LoginErrors.AccountNotVerified);
        }

        var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!isPasswordValid)
        {
            user.FailedLoginAttempts++;

            if (user.FailedLoginAttempts >= 5)
            {
                user.LockoutEnd = DateTime.UtcNow.AddMinutes(15);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return RequestResponse<AuthenticatedUserResult>.Fail(LoginErrors.InvalidCredentials);
        }

        user.FailedLoginAttempts = 0;
        user.LockoutEnd = null;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return RequestResponse<AuthenticatedUserResult>.Ok(
            new AuthenticatedUserResult(user.Id, user.Email, user.Role));
    }
}