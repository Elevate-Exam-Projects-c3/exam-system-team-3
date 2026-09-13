using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.SubmitQuestionAnswer.Queries;
using SendGrid.Helpers.Mail;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Handlers
{
    public class GetAttemptForAnswerQueryHandler(IGenericRepository<QuizAttempt> _attemptRepository) : IRequestHandler<GetAttemptForAnswerQuery,QuizAttempt?>
    {
        public async Task<QuizAttempt?> Handle(GetAttemptForAnswerQuery request,CancellationToken cancellationToken)
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
