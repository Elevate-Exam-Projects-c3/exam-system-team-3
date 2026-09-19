using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Shared.Results.ErrorCodes;

namespace exam_system.Features.Quizzes.AdminUnpublishQuiz.Commands;

public sealed class UnpublishQuizCommandHandler(IGenericRepository<Quiz> quizRepository,IUnitOfWork unitOfWork)
                                                : IRequestHandler<UnpublishQuizCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(UnpublishQuizCommand request,CancellationToken cancellationToken)
    {
        var quiz = await quizRepository
            .Get(x => x.Id == request.QuizId && !x.IsDeleted)
            .FirstOrDefaultAsync(cancellationToken);

        if (quiz is null)
            return QuizErrors.NotFound;

        quiz.Status = QuizStatus.Draft;
        quiz.PublishedAt = null;
        quiz.UpdatedAt = DateTime.UtcNow;

        quizRepository.Update(quiz);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Updated;
    }
}