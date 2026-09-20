using exam_system.Features.Attempts.SubmitAttempt.Commands;
using exam_system.Features.Attempts.SubmitAttempt.Orchestrators;
using exam_system.Features.Attempts.SubmitAttempt.Queries;
using exam_system.Features.Attempts.SubmitAttempt.Shared;
using exam_system.Features.Attempts.SubmitAttempt.ViewModels;

namespace exam_system.Features.Attempts.SubmitAttempt.Handlers
{
    public class SubmitQuizAttemptOrchestratorHandler : IRequestHandler<SubmitQuizAttemptOrchestrator,Result<SubmitQuizAttemptResponse>>
    {
        private readonly IMediator _mediator;
        private readonly IAttemptFinalizationService _finalizationService;

        public SubmitQuizAttemptOrchestratorHandler(
            IMediator mediator,
            IAttemptFinalizationService finalizationService)
        {
            _mediator = mediator;
            _finalizationService = finalizationService;
        }

        public async Task<Result<SubmitQuizAttemptResponse>> Handle(SubmitQuizAttemptOrchestrator request,CancellationToken cancellationToken)
        {
            var attempt = await _mediator.Send(
                new GetAttemptForSubmitQuery(
                    request.AttemptId,
                    request.StudentId),
                cancellationToken);

            if (attempt is null)
            {
                return Result<SubmitQuizAttemptResponse>.Failure(
                    Error.NotFound(
                        "ATTEMPT_NOT_FOUND",
                        "Quiz attempt was not found."));
            }

            if (attempt.Status != AttemptStatus.InProgress)
            {
                return Result<SubmitQuizAttemptResponse>.Failure(
                    Error.Conflict(
                        "ATTEMPT_ALREADY_FINISHED",
                        "Quiz attempt has already been submitted or timed out."));
            }

            var now = DateTime.UtcNow;

            // EXAM-135 Auto-Submit
            if (now >= attempt.Deadline)
            {
                return await _finalizationService.TimeoutAsync(
                    attempt.Id,
                    cancellationToken);
            }

            // EXAM-134 Explicit Submit
            return await _finalizationService.SubmitAsync(
                attempt.Id,
                cancellationToken);
        }
    }
}
