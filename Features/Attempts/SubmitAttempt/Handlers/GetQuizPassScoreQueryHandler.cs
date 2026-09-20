using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Attempts.SubmitAttempt.Queries;

namespace exam_system.Features.Attempts.SubmitAttempt.Handlers
{
    public sealed class GetQuizPassScoreQueryHandler(IGenericRepository<Quiz> _quizRepository) : IRequestHandler<GetQuizPassScoreQuery, int?>
    {
        public async Task<int?> Handle(GetQuizPassScoreQuery request,CancellationToken cancellationToken)
        {
            return await _quizRepository
                .Get(x =>
                    x.Id == request.QuizId &&
                    !x.IsDeleted)
                .Select(x => (int?)x.PassScore)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
