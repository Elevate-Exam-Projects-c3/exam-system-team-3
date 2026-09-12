using exam_system.Domain.Entities.Quizzes;

namespace exam_system.Features.Questions.AdminDeleteQuestion.Queries
{
    public record GetQuestionWithQuizQuery(Guid QuestionId) : IRequest<Question?>;
}
