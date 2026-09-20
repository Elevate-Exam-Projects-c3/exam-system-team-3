using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Questions.AdminUpdateQuestion.Queries;

namespace exam_system.Features.Questions.AdminUpdateQuestion.Handlers
{
    public sealed class GetQuestionOptionForUpdateQueryHandler(IGenericRepository<QuestionOption> _optionRepository) : IRequestHandler<GetQuestionOptionForUpdateQuery, QuestionOption?>
    {
        public async Task<QuestionOption?> Handle(GetQuestionOptionForUpdateQuery request,CancellationToken cancellationToken)
        {
            return await _optionRepository
                .Get(x =>
                    x.Id == request.OptionId &&
                    !x.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
