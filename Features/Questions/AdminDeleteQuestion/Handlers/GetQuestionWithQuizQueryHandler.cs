using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Questions.AdminDeleteQuestion.Queries;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Questions.AdminDeleteQuestion.Handlers
{
    public class GetQuestionWithQuizQueryHandler(IGenericRepository<Question> _questionRepo) : IRequestHandler<GetQuestionWithQuizQuery, Question?>
    {
        public async Task<Question?> Handle(GetQuestionWithQuizQuery request,CancellationToken cancellationToken)
        {
            return await _questionRepo.GetByIdAsync(
                request.QuestionId,q => q.Quiz);
        }
    }
}
