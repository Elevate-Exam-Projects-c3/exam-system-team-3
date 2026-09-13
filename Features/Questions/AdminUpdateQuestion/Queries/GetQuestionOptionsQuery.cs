using exam_system.Domain.Entities.Quizzes;

namespace exam_system.Features.Questions.AdminUpdateQuestion.Queries
{
    public record GetQuestionOptionsQuery(Guid QuestionId) : IRequest<List<QuestionOption>>;
}
