using exam_system.Domain.Entities.Quizzes;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Quizzes.AdminCreateQuiz.Queries
{
    public record CheckIfExist(string QuizTitle,Guid DiplomaId) : IRequest<bool>;

    public class CheckIfExistHandler : IRequestHandler<CheckIfExist, bool>
    {
        private readonly IGenericRepository<Quiz> _quizRepo;

        public CheckIfExistHandler(IGenericRepository<Quiz> quizRepo)
        {
            _quizRepo = quizRepo;
        }

        public async Task<bool> Handle(CheckIfExist request,CancellationToken cancellationToken)
        {
            return await _quizRepo.ExistsAsync(q => q.DiplomaId == request.DiplomaId && q.Title == request.QuizTitle,
                cancellationToken);
        }
    }
}
