using exam_system.Domain.Entities.Identity;

namespace exam_system.Features.Dashboard.AdminDashboard.Handlers;

public class GetActiveUsersTodayQueryHandler(IGenericRepository<RefreshToken> refreshTokenRepository) : IRequestHandler<GetActiveUsersTodayQuery, int>
{
    public async Task<int> Handle(GetActiveUsersTodayQuery request, CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow.Date;
        var tomorrow = today.AddDays(1);
        return await refreshTokenRepository
          .Get(refreshToken =>
              refreshToken.CreatedAt >= today &&
              refreshToken.CreatedAt < tomorrow)
          .Select(refreshToken => refreshToken.UserId)
          .Distinct()
          .CountAsync(cancellationToken);
    }
}
