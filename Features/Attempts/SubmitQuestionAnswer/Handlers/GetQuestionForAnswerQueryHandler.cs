using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Attempts.SubmitQuestionAnswer.Queries;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Handlers
{
    public sealed class GetQuestionForAnswerQueryHandler(IGenericRepository<Question> _questionRepository): IRequestHandler<GetQuestionForAnswerQuery,Question?>
    {
        public async Task<Question?> Handle(GetQuestionForAnswerQuery request,CancellationToken cancellationToken)
        {
            return await _questionRepository
                .Get(x =>
                    x.Id == request.QuestionId &&
                    x.QuizId == request.QuizId &&
                    !x.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
