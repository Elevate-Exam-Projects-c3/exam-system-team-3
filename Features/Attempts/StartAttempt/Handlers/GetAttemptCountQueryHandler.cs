using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.StartAttempt.Queries;

namespace exam_system.Features.Attempts.StartAttempt.Handlers
{
    public class GetAttemptCountQueryHandler(IGenericRepository<QuizAttempt> _attemptRepository): IRequestHandler<GetAttemptCountQuery, int>
    {
        public async Task<int> Handle(GetAttemptCountQuery request,CancellationToken cancellationToken)
        {
            return await _attemptRepository.CountAsync(x =>
                    x.StudentId == request.StudentId &&
                    x.QuizId == request.QuizId &&
                    (
                        x.Status == AttemptStatus.Submitted ||
                        x.Status == AttemptStatus.TimedOut
                    ) &&
                    !x.IsDeleted);
        }
    }
}
