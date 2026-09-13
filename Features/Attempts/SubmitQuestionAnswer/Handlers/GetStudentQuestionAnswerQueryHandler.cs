using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.SubmitQuestionAnswer.Queries;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Handlers
{
    public sealed class GetStudentQuestionAnswerQueryHandler(IGenericRepository<StudentQuestionAnswer> _answerRepository): IRequestHandler<GetStudentQuestionAnswerQuery,StudentQuestionAnswer?>
    {
        public async Task<StudentQuestionAnswer?> Handle(GetStudentQuestionAnswerQuery request,CancellationToken cancellationToken)
        {
            return await _answerRepository
                .Get(x =>
                    x.AttemptId == request.AttemptId &&
                    x.QuestionId == request.QuestionId &&
                    !x.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
