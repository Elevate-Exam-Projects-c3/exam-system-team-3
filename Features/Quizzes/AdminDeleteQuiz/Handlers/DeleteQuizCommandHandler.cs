using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Quizzes.AdminDeleteQuiz.Commands;
using exam_system.Features.Shared.Results;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Quizzes.AdminDeleteQuiz.Handlers
{
    public class DeleteQuizCommandHandler(IGenericRepository<Quiz> _quizRepo) : IRequestHandler<DeleteQuizCommand,Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(DeleteQuizCommand request,CancellationToken cancellationToken)
        {
            var quiz = request.Quiz;

            _quizRepo.Delete(quiz);

            await _quizRepo.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(quiz.Id);
        }
    }
}
