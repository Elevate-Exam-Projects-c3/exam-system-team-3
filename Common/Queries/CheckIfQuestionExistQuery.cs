using exam_system.Domain.Entities.Quizzes;
using exam_system.Persistence.DataAccess;

namespace exam_system.Common.Queries
{
    public record CheckIfQuestionExistQuery(Guid QuizId,string QuestionText,Guid? ExcludeQuestionId = null) : IRequest<bool>;

    public class CheckIfQuestionExistQueryHandler(IGenericRepository<Question> _questionRepo) : IRequestHandler<CheckIfQuestionExistQuery, bool>
    {
        public async Task<bool> Handle(CheckIfQuestionExistQuery request,CancellationToken cancellationToken)
        {
            var text = request.QuestionText.Trim();

            return await _questionRepo.ExistsAsync(
                q =>
                    q.QuizId == request.QuizId &&
                    q.Text == text &&
                    (!request.ExcludeQuestionId.HasValue ||
                     q.Id != request.ExcludeQuestionId.Value),
                cancellationToken);
        }
    }
}
