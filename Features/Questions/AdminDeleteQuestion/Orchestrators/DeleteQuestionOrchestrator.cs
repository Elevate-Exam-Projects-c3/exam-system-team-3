using exam_system.Features.Shared.Results;

namespace exam_system.Features.Questions.AdminDeleteQuestion.Orchestrators
{
    public record DeleteQuestionOrchestrator(Guid QuizId,Guid QuestionId) : IRequest<Result<Guid>>;
}
