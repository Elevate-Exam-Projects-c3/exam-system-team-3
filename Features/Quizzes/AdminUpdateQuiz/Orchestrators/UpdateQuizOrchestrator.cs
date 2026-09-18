using exam_system.Common.Queries;
using exam_system.Features.Quizzes.AdminUpdateQuiz.Commands;
using exam_system.Features.Quizzes.AdminUpdateQuiz.DTO;
using exam_system.Features.Shared.Results;

namespace exam_system.Features.Quizzes.AdminUpdateQuiz.Orchestrators
{
    public record UpdateQuizOrchestrator(Guid QuizId, UpdateQuizRequest Request) : IRequest<Result<Guid>>;
}


