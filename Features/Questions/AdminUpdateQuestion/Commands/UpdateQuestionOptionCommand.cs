using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Questions.AdminUpdateQuestion.ViewModels;
using exam_system.Features.Shared;
using exam_system.Features.Shared.Results;

namespace exam_system.Features.Questions.AdminUpdateQuestion.Commands
{
    public sealed record UpdateQuestionOptionCommand(
     QuestionOption Option,
     UpdateQuestionOptionRequest Request)
     : IRequest<Result<Guid>>;
}
