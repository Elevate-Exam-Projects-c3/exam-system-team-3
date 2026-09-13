namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Commands
{
    public record UpdateQuestionAnswerCommand(Guid AnswerId,Guid SelectedOptionId,bool IsCorrect) : IRequest<Result<Guid>>;
}
