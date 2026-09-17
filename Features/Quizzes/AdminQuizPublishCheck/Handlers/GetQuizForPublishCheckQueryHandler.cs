using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Quizzes.AdminQuizPublishCheck.Commands;
using exam_system.Features.Quizzes.AdminQuizPublishCheck.DTOs;
using exam_system.Features.Shared.Results.ErrorCodes;

namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.Handlers;

public class GetQuizForPublishCheckQueryHandler(IGenericRepository<Quiz> quizRepository) : IRequest<Result<PublishCheckData>>
{
    public async Task<Result<PublishCheckData>> Handle(GetQuizForPublishCheckQuery request, CancellationToken cancellationToken)
    {
        var quiz = await quizRepository
         .Get(x => x.Id == request.QuizId && !x.IsDeleted)
         .Select(x => new PublishCheckData(x.Questions.Count(question => !question.IsDeleted),
             x.Questions
                 .Where(question => !question.IsDeleted)
                 .Select(question => question.Options
                 .Count(option => !option.IsDeleted && option.IsCorrect))
                 .ToList(), x.DurationMinutes, x.PassScore))
         .FirstOrDefaultAsync(cancellationToken);

        if (quiz is null)
            return QuizErrors.NotFound;

        return quiz;
    }
}
