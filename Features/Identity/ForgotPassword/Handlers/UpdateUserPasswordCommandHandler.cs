using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.ForgotPassword.Commands;
using exam_system.Features.Shared.Results.ErrorCodes;

namespace exam_system.Features.Identity.ForgotPassword.Handlers;

public class UpdateUserPasswordCommandHandler(
    IGenericRepository<ApplicationUser> userRepo,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateUserPasswordCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(
        UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepo.GetByIdAsync(request.UserId);

        if (user is null)
        {
            return ResetPasswordErrors.InvalidResetToken;
        }

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword, workFactor: 12);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Updated;        
    }
}