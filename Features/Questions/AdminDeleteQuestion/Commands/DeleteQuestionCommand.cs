using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Shared.Results;

namespace exam_system.Features.Questions.AdminDeleteQuestion.Commands
{
    public record DeleteQuestionCommand(Question Question) : IRequest<Result<Guid>>;
}
