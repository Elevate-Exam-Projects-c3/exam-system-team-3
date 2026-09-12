using exam_system.Domain.Entities.Quizzes;

namespace exam_system.Features.Questions.AdminUpdateQuestion.Queries
{
    public record GetQuestionQuery(Guid QuestionId : IRequest<Question?>;
}
