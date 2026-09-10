using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Quizzes.AdminCreateQuiz.Queries;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Quizzes.AdminUpdateQuiz.Queries
{
    public record CheckIfExistQuery(string QuizTitle,Guid? ExcludeQuizId = null) : IRequest<bool>;
    public class CheckIfExistHandler(IGenericRepository<Quiz> _quizRepo) : IRequestHandler<CheckIfExistQuery, bool>
    {
        public async Task<bool> Handle(CheckIfExistQuery request,CancellationToken cancellationToken)
        {
            return await _quizRepo.ExistsAsync( q =>
                    q.Title == request.QuizTitle.Trim() && (!request.ExcludeQuizId.HasValue || q.Id != request.ExcludeQuizId.Value), cancellationToken);
        }
    }
}
