using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Attempts.SubmitQuestionAnswer.Queries;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Handlers
{
    public sealed class GetQuestionOptionForAnswerQueryHandler(IGenericRepository<QuestionOption> _optionRepository): IRequestHandler<GetQuestionOptionForAnswerQuery,QuestionOption?>
    {
        public async Task<QuestionOption?> Handle(GetQuestionOptionForAnswerQuery request,CancellationToken cancellationToken)
        {
            return await _optionRepository
                .Get(x =>
                    x.Id == request.OptionId &&
                    x.QuestionId == request.QuestionId &&
                    !x.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
