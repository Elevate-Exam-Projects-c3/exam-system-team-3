using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.Register.Queries;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Identity.Register.Handlers;

public class CheckEmailExistsQueryHandler(
    IGenericRepository<ApplicationUser> userRepo) 
    : IRequestHandler<CheckEmailExistsQuery, bool>
{
    public async Task<bool> Handle(
        CheckEmailExistsQuery request, 
        
        CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.ToLower();

        return await userRepo
            .Get(u => u.Email == normalizedEmail)
            .AnyAsync(cancellationToken);
    }
}