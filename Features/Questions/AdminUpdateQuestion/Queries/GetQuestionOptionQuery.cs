using exam_system.Domain.Entities.Quizzes;

namespace exam_system.Features.Questions.AdminUpdateQuestion.Queries
{
    public record GetQuestionOptionQuery(Guid OptionId) : IRequest<QuestionOption?>;
}
