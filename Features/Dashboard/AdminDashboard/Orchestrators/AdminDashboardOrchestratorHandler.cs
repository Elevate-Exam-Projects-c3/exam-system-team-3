using exam_system.Features.Dashboard.AdminDashboard.DTOs;
using exam_system.Features.Dashboard.AdminDashboard.Orchestrators;

namespace exam_system.Features.Analytics.GetDashboard.Handlers;

public sealed class AdminDashboardOrchestratorHandler(ISender sender,IMemoryCache cache)
                                                    : IRequestHandler<AdminDashboardOrchestrator, Result<AdminDashboardResponse>>
{
    private const string _cacheKey = "admin-dashboard-snapshot";
    private static readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

    public async Task<Result<AdminDashboardResponse>> Handle(AdminDashboardOrchestrator request,CancellationToken cancellationToken)
    {
        if (cache.TryGetValue(_cacheKey,out AdminDashboardResponse? cachedDashboard)&& cachedDashboard is not null)
        {
            return Result<AdminDashboardResponse>.Success(cachedDashboard);
        }

        var totalRegisteredUsers = await sender.Send(new GetTotalRegisteredUsersQuery(),cancellationToken);

        var activeUsersToday = await sender.Send(new GetActiveUsersTodayQuery(),cancellationToken);

        var totalDiplomas = await sender.Send(new GetTotalDiplomasQuery(),cancellationToken);

        var totalQuizzes = await sender.Send(new GetTotalQuizzesQuery(),cancellationToken);

        var totalAttempts = await sender.Send(new GetTotalAttemptsQuery(),cancellationToken);

        var overallAveragePassRate = await sender.Send(new GetOverallPassRateQuery(),cancellationToken);

        var dashboard = new AdminDashboardResponse(
            totalRegisteredUsers,
            activeUsersToday,
            totalDiplomas,
            totalQuizzes,
            totalAttempts,
            overallAveragePassRate);

        cache.Set(_cacheKey,dashboard,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheDuration
            });

        return Result<AdminDashboardResponse>.Success(dashboard);
    }
}