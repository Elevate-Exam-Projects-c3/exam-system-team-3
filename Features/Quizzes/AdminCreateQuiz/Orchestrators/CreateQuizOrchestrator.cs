using exam_system.Common.Queries;
using exam_system.Features.Quizzes.AdminCreateQuiz.Commands;
using exam_system.Features.Quizzes.AdminCreateQuiz.ViewModel;
using exam_system.Features.Shared;
using exam_system.Features.Shared.Results;

namespace exam_system.Features.Quizzes.AdminCreateQuiz.Orchestrators
{
    public record CreateQuizOrchestrator(CreateQuizRequest Request) : IRequest<Result<Guid>>;
}
