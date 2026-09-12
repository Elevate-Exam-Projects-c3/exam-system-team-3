using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Questions.AdminUpdateQuestion.Queries;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Questions.AdminUpdateQuestion.Handlers
{
    public class GetQuestionOptionQueryHandler(IGenericRepository<QuestionOption> _optionRepo): IRequestHandler<GetQuestionOptionQuery, QuestionOption?>
    {
        public async Task<QuestionOption?> Handle(GetQuestionOptionQuery request,CancellationToken cancellationToken)
        {
            return await _optionRepo.GetByIdAsync(
                request.OptionId,
                x => x.Question);
        }
    }
}
