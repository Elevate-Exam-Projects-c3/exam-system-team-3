using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Shared.Results.ErrorCodes;

namespace exam_system.Features.Quizzes.AdminPublishQuiz.Commands;

public sealed class PublishQuizCommandHandler(IGenericRepository<Quiz> quizRepository,IUnitOfWork unitOfWork)
                                            : IRequestHandler<PublishQuizCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(PublishQuizCommand request,CancellationToken cancellationToken)
    {
        var quiz = await quizRepository
            .Get(x => x.Id == request.QuizId && !x.IsDeleted)
            .FirstOrDefaultAsync(cancellationToken);

        if (quiz is null)
            return QuizErrors.NotFound;

        quiz.Status = QuizStatus.Published;
        quiz.PublishedAt = DateTime.UtcNow;
        quiz.UpdatedAt = DateTime.UtcNow;

        quizRepository.Update(quiz);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Updated;
    }
}