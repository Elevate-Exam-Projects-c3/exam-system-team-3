using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Attempts.SubmitAttempt.Queries;

namespace exam_system.Features.Attempts.SubmitAttempt.Handlers
{
    public sealed class GetQuizQuestionCountQueryHandler(IGenericRepository<Question> _questionRepository) : IRequestHandler<GetQuizQuestionCountQuery, int>
    {
        public async Task<int> Handle(GetQuizQuestionCountQuery request,CancellationToken cancellationToken)
        {
            return await _questionRepository
                .Get(x =>
                    x.QuizId == request.QuizId &&
                    !x.IsDeleted)
                .CountAsync(cancellationToken);
        }
    }
}
