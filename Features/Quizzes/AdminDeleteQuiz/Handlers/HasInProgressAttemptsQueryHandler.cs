using exam_system.Common.Enums;
using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Quizzes.AdminDeleteQuiz.Queries;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Quizzes.AdminDeleteQuiz.Handlers
{
    public class HasInProgressAttemptsQueryHandler(IGenericRepository<QuizAttempt> _attemptRepo): IRequestHandler<HasInProgressAttemptsQuery, bool>
    {
        public async Task<bool> Handle(HasInProgressAttemptsQuery request,CancellationToken cancellationToken)
        {
            return await _attemptRepo.ExistsAsync(x =>
                    x.QuizId == request.QuizId &&
                    x.Status == AttemptStatus.InProgress,
                cancellationToken);
        }
    }
}
