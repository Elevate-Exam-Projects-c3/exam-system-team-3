using exam_system.Features.Quizzes.AdminCreateQuiz.Commands;
using exam_system.Features.Quizzes.AdminCreateQuiz.Queries;

namespace exam_system.Features.Quizzes.AdminCreateQuiz.Orchestrators
{
    public class CreateQuizOrchestrator(IMediator _mediator)
    {
        public async Task<Guid> ExecuteAsync(CreateQuizCommand command,CancellationToken cancellationToken)
        {            
            var exists = await _mediator.Send(new CheckIfExist(command.Title,command.DiplomaId),cancellationToken);
            
            if (exists)
            {
                throw new Exception(
                    "A quiz with this title already exists under this diploma.");
            }
            
            var quizId = await _mediator.Send(command,cancellationToken);

            return quizId;
        }
    }
}
