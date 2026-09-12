using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Questions.AdminUpdateQuestion.ViewModels;
using exam_system.Features.Shared.Results;

namespace exam_system.Features.Questions.AdminUpdateQuestion.Commands
{
    public record UpdateQuestionCommand(Question Question,UpdateQuestionRequest Request) : IRequest<Result<Guid>>;
}
