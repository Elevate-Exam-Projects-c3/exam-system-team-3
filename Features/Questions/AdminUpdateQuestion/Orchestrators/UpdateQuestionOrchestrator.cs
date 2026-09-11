using exam_system.Features.Questions.AdminUpdateQuestion.ViewModels;
using exam_system.Features.Shared.Results;

namespace exam_system.Features.Questions.AdminUpdateQuestion.Orchestrators
{
    public record UpdateQuestionOrchestrator(Guid QuizId,Guid QuestionId,UpdateQuestionRequest Request) : IRequest<Result<Guid>>;
}
