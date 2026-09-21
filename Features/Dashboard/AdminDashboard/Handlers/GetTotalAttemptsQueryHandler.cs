using exam_system.Domain.Entities.Attempts;

namespace exam_system.Features.Dashboard.AdminDashboard.Handlers;

public class GetTotalAttemptsQueryHandler (IGenericRepository<QuizAttempt> attemptRepository)
                                          : IRequestHandler<GetTotalAttemptsQuery, int>
{
    public Task<int> Handle(GetTotalAttemptsQuery request, CancellationToken cancellationToken)
    => attemptRepository.GetAll().CountAsync(cancellationToken);
}
