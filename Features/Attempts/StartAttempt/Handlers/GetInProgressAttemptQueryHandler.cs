using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.StartAttempt.Queries;

namespace exam_system.Features.Attempts.StartAttempt.Handlers
{
    public class GetInProgressAttemptQueryHandler(IGenericRepository<QuizAttempt> _attemptRepository) : IRequestHandler<GetInProgressAttemptQuery,QuizAttempt?>
    {
        public async Task<QuizAttempt?> Handle(GetInProgressAttemptQuery request,CancellationToken cancellationToken)
        {
            return await _attemptRepository
                .Get(x =>
                    x.StudentId == request.StudentId &&
                    x.QuizId == request.QuizId &&
                    x.Status == AttemptStatus.InProgress &&
                    !x.IsDeleted)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
