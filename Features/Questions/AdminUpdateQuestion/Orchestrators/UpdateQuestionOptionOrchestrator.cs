using exam_system.Features.Questions.AdminUpdateQuestion.ViewModels;
using exam_system.Features.Shared.Results;

namespace exam_system.Features.Questions.AdminUpdateQuestion.Orchestrators
{
    public sealed record UpdateQuestionOptionOrchestrator(
        Guid QuizId,
        Guid QuestionId,
        Guid OptionId,
        UpdateQuestionOptionRequest Request): IRequest<Result<Guid>>;
}
