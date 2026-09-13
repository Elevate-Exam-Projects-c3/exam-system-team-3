using exam_system.Domain.Entities.Attempts;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Commands
{
    public record SubmitQuestionAnswerCommand(Guid AttemptId,Guid QuestionId,Guid SelectedOptionId,bool IsCorrect) : IRequest<Result<Guid>>;
}
