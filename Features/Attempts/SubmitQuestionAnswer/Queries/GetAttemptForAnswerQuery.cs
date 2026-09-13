using exam_system.Domain.Entities.Attempts;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Queries
{
    public record GetAttemptForAnswerQuery(Guid AttemptId,Guid StudentId) : IRequest<QuizAttempt?>;
}
