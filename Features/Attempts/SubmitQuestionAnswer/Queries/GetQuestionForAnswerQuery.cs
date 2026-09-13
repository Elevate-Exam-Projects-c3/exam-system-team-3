using exam_system.Domain.Entities.Quizzes;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Queries
{
    public record GetQuestionForAnswerQuery(Guid QuestionId,Guid QuizId) : IRequest<Question?>;
}
