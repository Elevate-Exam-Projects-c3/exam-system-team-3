using exam_system.Domain.Entities.Quizzes;

namespace exam_system.Features.Questions.AdminUpdateQuestion.Queries
{
    public record GetQuestionOptionForUpdateQuery(Guid OptionId) : IRequest<QuestionOption?>;
}
