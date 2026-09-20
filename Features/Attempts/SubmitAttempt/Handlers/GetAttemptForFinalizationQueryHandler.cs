using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.SubmitAttempt.Queries;

namespace exam_system.Features.Attempts.SubmitAttempt.Handlers
{
    public sealed class GetAttemptForFinalizationQueryHandler(IGenericRepository<QuizAttempt> _attemptRepository)  : IRequestHandler<GetAttemptForFinalizationQuery,QuizAttempt?>
    {
        public async Task<QuizAttempt?> Handle(GetAttemptForFinalizationQuery request,CancellationToken cancellationToken)
        {
            return await _attemptRepository
                .Get(x =>
                    x.Id == request.AttemptId &&
                    !x.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
