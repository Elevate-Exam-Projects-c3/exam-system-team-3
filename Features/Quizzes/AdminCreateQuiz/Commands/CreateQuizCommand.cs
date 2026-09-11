using exam_system.Common.Enums;
using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Quizzes.AdminCreateQuiz.ViewModel;
using exam_system.Features.Shared;
using exam_system.Features.Shared.Results;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Quizzes.AdminCreateQuiz.Commands
{
    public record CreateQuizCommand(CreateQuizRequest Request) : IRequest<Result<Guid>>;
    
}
