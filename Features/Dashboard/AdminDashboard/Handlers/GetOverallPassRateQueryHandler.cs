using exam_system.Domain.Entities.Attempts;

namespace exam_system.Features.Dashboard.AdminDashboard.Handlers;

public class GetOverallPassRateQueryHandler(IGenericRepository<QuizAttempt> attemptRepository)
                                            : IRequestHandler<GetOverallPassRateQuery, double>
{
    public async Task<double> Handle(GetOverallPassRateQuery request, CancellationToken cancellationToken)
    {
        var completedAttempts = attemptRepository
           .Get(attempt => attempt.Passed.HasValue);
        var totalCompletedAttempts = await completedAttempts
            .CountAsync(cancellationToken);
        if (totalCompletedAttempts == 0)
            return 0;
        var passedAttempts = await completedAttempts
         .CountAsync(attempt => attempt.Passed == true,cancellationToken);
        return Math.Round(passedAttempts * 100.0 / totalCompletedAttempts, 2);
    }
}
