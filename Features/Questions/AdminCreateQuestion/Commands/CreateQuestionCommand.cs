using exam_system.Features.Questions.AdminCreateQuestion.ViewModels;
using exam_system.Features.Shared.Results;

namespace exam_system.Features.Questions.AdminCreateQuestion.Commands
{
    public record CreateQuestionCommand(Guid QuizId,CreateQuestionRequest Request) : IRequest<Result<Guid>>;

}
