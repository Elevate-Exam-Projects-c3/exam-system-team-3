using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Questions.AdminDeleteQuestion.Commands;
using exam_system.Features.Shared.Results;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Questions.AdminDeleteQuestion.Handlers
{
    public class DeleteQuestionCommandHandler(IGenericRepository<Question> _questionRepo) : IRequestHandler<DeleteQuestionCommand,Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(DeleteQuestionCommand request,CancellationToken cancellationToken)
        {
            var question = request.Question;

            question.IsDeleted = true;
            question.DeletedAt = DateTime.UtcNow;

            await _questionRepo.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(question.Id);
        }
    }
}
