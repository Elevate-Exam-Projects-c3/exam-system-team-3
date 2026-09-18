using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.SubmitAttempt.Queries;

namespace exam_system.Features.Attempts.SubmitAttempt.Handlers
{
    public class GetAttemptForSubmitQueryHandler(IGenericRepository<QuizAttempt> _attemptRepository) : IRequestHandler<GetAttemptForSubmitQuery, QuizAttempt?>
    {
        public async Task<QuizAttempt?> Handle(GetAttemptForSubmitQuery request,CancellationToken cancellationToken)
        {
            return await _attemptRepository
                .Get(x =>
                    x.Id == request.AttemptId &&
                    x.StudentId == request.StudentId &&
                    !x.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
