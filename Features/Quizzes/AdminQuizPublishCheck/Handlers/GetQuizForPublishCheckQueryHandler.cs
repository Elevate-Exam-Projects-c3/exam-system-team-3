using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Quizzes.AdminQuizPublishCheck.Commands;
using exam_system.Features.Shared.Results.ErrorCodes;

namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.Handlers;

public class GetQuizForPublishCheckQueryHandler(IGenericRepository<Quiz> quizRepository) : IRequest<Result<Quiz>>
{
    public async Task<Result<Quiz>> Handle(GetQuizForPublishCheckQuery request,CancellationToken cancellationToken)
    {
        var quiz = await quizRepository
            .Get(x => x.Id == request.QuizId && !x.IsDeleted)
            .Include(x => x.Questions.Where(question => !question.IsDeleted))
            .ThenInclude(question =>question.Options.Where(option => !option.IsDeleted))
            .FirstOrDefaultAsync(cancellationToken);

        if (quiz is null)
            return QuizErrors.NotFound;

        return quiz;
    }
}
