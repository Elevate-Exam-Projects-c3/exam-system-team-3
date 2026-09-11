using exam_system.Common.Enums;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.Register.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Identity.Register.Handlers;

public class CreateUserCommandHandler(
    IGenericRepository<ApplicationUser> userRepo)
    : IRequestHandler<CreateUserCommand, RequestResponse<Guid>>
{
    public async Task<RequestResponse<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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

        return RequestResponse<Guid>.Ok(user.Id);
    }
}