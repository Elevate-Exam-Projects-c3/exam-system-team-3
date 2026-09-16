using exam_system.Common.Enums;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.Register.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Identity.Register.Handlers;

public class CreateUserCommandHandler(
    IGenericRepository<ApplicationUser> userRepo)
    : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor: 12);

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = passwordHash,
            Role = UserRole.Student,
            AccountStatus = AccountStatus.Pending,
            EmailConfirmed = false,
            CreatedAt = DateTime.UtcNow
        };

        await userRepo.AddAsync(user);

        return Result<Guid>.Success(user.Id);
    }
}