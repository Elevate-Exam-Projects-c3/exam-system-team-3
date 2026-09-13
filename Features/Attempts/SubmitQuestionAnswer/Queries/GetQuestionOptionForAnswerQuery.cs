using exam_system.Domain.Entities.Quizzes;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Queries
{
    public record GetQuestionOptionForAnswerQuery(Guid QuestionId,Guid OptionId) : IRequest<QuestionOption?>;
}
