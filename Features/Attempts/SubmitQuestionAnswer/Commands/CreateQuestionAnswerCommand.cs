namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Commands
{
    public record CreateQuestionAnswerCommand(Guid AttemptId,Guid QuestionId,Guid SelectedOptionId,bool IsCorrect) : IRequest<Result<Guid>>;
}
