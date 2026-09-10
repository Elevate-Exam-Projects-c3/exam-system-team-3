using exam_system.Domain.Entities.Quizzes;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Quizzes.AdminUpdateQuiz.Queries
{
    public record GetQuizQuery(Guid QuizId) : IRequest<Quiz?>;

    public class GetQuizQueryHandler(IGenericRepository<Quiz> _quizRepo) : IRequestHandler<GetQuizQuery, Quiz?>
    {
        public async Task<Quiz?> Handle(GetQuizQuery request,CancellationToken cancellationToken)
        {
            return await _quizRepo.GetByIdAsync(request.QuizId);
        }
    }
}
