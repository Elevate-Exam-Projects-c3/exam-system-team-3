using exam_system.Features.Attempts.SubmitQuestionAnswer.ViewModels;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Orchestrators
{
    public record SubmitQuestionAnswerOrchestrator(Guid AttemptId,Guid QuestionId,Guid StudentId,SubmitQuestionAnswerRequest Request) : IRequest<Result<SubmitQuestionAnswerResponse>>;
}
