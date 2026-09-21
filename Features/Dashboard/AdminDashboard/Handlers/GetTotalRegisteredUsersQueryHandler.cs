using exam_system.Domain.Entities.Identity;

namespace exam_system.Features.Dashboard.AdminDashboard.Handlers;

public class GetTotalRegisteredUsersQueryHandler(IGenericRepository<ApplicationUser> userRepository)
                                                : IRequestHandler<GetTotalRegisteredUsersQuery, int>
{
    public async Task<int> Handle(GetTotalRegisteredUsersQuery request, CancellationToken cancellationToken)
        => await userRepository
                   .GetAll()
                   .CountAsync(cancellationToken);
}
