using exam_system.Domain.Entities.Attempts;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Queries
{
    public record GetStudentQuestionAnswerQuery(Guid AttemptId,Guid QuestionId) : IRequest<StudentQuestionAnswer?>;
}
