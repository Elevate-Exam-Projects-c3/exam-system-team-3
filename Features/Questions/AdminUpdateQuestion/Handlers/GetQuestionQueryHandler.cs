using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Questions.AdminUpdateQuestion.Queries;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Questions.AdminUpdateQuestion.Handlers
{
    public class GetQuestionQueryHandler(IGenericRepository<Question> _questionRepo): IRequestHandler<GetQuestionQuery, Question?>
    {
        public async Task<Question?> Handle(GetQuestionQuery request,CancellationToken cancellationToken)
        {
            return await _questionRepo.GetByIdAsync(
                request.QuestionId,
                q => q.Options,
                q => q.Quiz);
        }
    }
}
