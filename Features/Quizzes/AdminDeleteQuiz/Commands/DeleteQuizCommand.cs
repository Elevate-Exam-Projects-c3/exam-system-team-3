using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Shared.Results;

namespace exam_system.Features.Quizzes.AdminDeleteQuiz.Commands
{
    public record DeleteQuizCommand(Quiz Quiz) : IRequest<Result<Guid>>;
}
