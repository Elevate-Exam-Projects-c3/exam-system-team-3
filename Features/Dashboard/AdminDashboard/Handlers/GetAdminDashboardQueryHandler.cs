using exam_system.Features.Dashboard.AdminDashboard.DTOs;
using exam_system.Features.Dashboard.AdminDashboard.Queries;

namespace exam_system.Features.Dashboard.AdminDashboard.Handlers;

public sealed class GetAdminDashboardQueryHandler(AppDbContext dbContext,IMemoryCache cache)
                                                  : IRequestHandler<GetAdminDashboardQuery, Result<AdminDashboardResponse>>
{
    private const string _cacheKey = "admin-dashboard-snapshot";
    private static readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);
    public async Task<Result<AdminDashboardResponse>> Handle(GetAdminDashboardQuery request,CancellationToken cancellationToken)
    {
        if (cache.TryGetValue(_cacheKey,out AdminDashboardResponse? cachedDashboard)&& cachedDashboard is not null)
            return Result<AdminDashboardResponse>.Success(cachedDashboard);

        var today = DateTime.UtcNow.Date;
        var tomorrow = today.AddDays(1);

        var totalRegisteredUsers = await dbContext.Users
            .AsNoTracking()
            .CountAsync(cancellationToken);

        var activeUsersToday = await dbContext.RefreshTokens
            .AsNoTracking()
            .Where(refreshToken =>
                refreshToken.CreatedAt >= today &&
                refreshToken.CreatedAt < tomorrow)
            .Select(refreshToken => refreshToken.UserId)
            .Distinct()
            .CountAsync(cancellationToken);

        var totalDiplomas = await dbContext.Diplomas
            .AsNoTracking()
            .CountAsync(cancellationToken);

        var totalQuizzes = await dbContext.Quizzes
            .AsNoTracking()
            .CountAsync(cancellationToken);

        var totalAttempts = await dbContext.QuizAttempts
            .AsNoTracking()
            .CountAsync(cancellationToken);

        var completedAttempts = dbContext.QuizAttempts
            .AsNoTracking()
            .Where(attempt => attempt.Passed.HasValue);

        var completedAttemptsCount = await completedAttempts
            .CountAsync(cancellationToken);

        var passedAttemptsCount = await completedAttempts
            .CountAsync(attempt => attempt.Passed == true,cancellationToken);

        var overallAveragePassRate = completedAttemptsCount == 0? 0: Math.Round(passedAttemptsCount * 100.0 / completedAttemptsCount,2);

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