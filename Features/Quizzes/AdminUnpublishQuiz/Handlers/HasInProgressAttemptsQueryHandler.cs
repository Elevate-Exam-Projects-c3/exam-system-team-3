using exam_system.Domain.Entities.Attempts;
using exam_system.Domain.Entities.Quizzes;

namespace exam_system.Features.Quizzes.AdminUnpublishQuiz.Queries;

public sealed class HasInProgressAttemptsQueryHandler(IGenericRepository<QuizAttempt> attemptRepository)
                                                     : IRequestHandler<HasInProgressAttemptsQuery, bool>
{
    public async Task<bool> Handle(HasInProgressAttemptsQuery request,CancellationToken cancellationToken)
    {
        return await attemptRepository
            .Get(attempt =>
                attempt.QuizId == request.QuizId &&
                attempt.Status == AttemptStatus.InProgress)
            .AnyAsync(cancellationToken);
    }
}