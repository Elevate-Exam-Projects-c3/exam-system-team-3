using exam_system.Features.Attempts.CheckRemainingTime.DTOs;
using exam_system.Features.Attempts.CheckRemainingTime.Orchestrators;
using exam_system.Features.Attempts.CheckRemainingTime.Queries;

namespace exam_system.Features.Attempts.CheckRemainingTime.Handlers
{
    public class CheckRemainingTimeOrchestratorHandler(IMediator _mediator): IRequestHandler<CheckRemainingTimeOrchestrator,Result<RemainingTimeResponse>>
    {
        public async Task<Result<RemainingTimeResponse>> Handle(CheckRemainingTimeOrchestrator request,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new CheckRemainingTimeQuery(
                    request.AttemptId,
                    request.StudentId),
                cancellationToken);

            if (!result.IsSuccess)
            {
                return Result<RemainingTimeResponse>.Failure(
                    result.Errors[0]);
            }

            return result;
        }
    }
}
