using exam_system.Domain.Entities.Quizzes;

namespace exam_system.Features.Dashboard.AdminDashboard.Handlers;

public class GetTotalQuizzesQueryHandler(IGenericRepository<Quiz> quizRepository) : IRequestHandler<GetTotalQuizzesQuery, int>
{
    public Task<int> Handle(GetTotalQuizzesQuery request, CancellationToken cancellationToken)
    => quizRepository.GetAll().CountAsync(cancellationToken);
}
