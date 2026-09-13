using exam_system.Features.Attempts.GetAttemptResults.Orchestrators;
using exam_system.Features.Attempts.GetAttemptResults.Queries;
using exam_system.Features.Attempts.GetAttemptResults.ViewModels;

namespace exam_system.Features.Attempts.GetAttemptResults.Handlers
{
    public sealed class GetAttemptResultsOrchestratorHandler(IMediator _mediator) : IRequestHandler<GetAttemptResultsOrchestrator, Result<GetAttemptResultsResponse>>
    {
        public async Task<Result<GetAttemptResultsResponse>> Handle(GetAttemptResultsOrchestrator request, CancellationToken cancellationToken)
        { // 1. Get attempt and verify ownership
          var attempt = await _mediator.Send( new GetAttemptForResultsQuery( request.AttemptId, request.StudentId), cancellationToken); 
            if (attempt is null)
            {
                return Result<GetAttemptResultsResponse>.Failure( Error.NotFound( "ATTEMPT_NOT_FOUND", "Quiz attempt was not found.")); } 
            // 2. Results are available only after submission or timeout
            if (attempt.Status != AttemptStatus.Submitted && attempt.Status != AttemptStatus.TimedOut) 
            {
                return Result<GetAttemptResultsResponse>.Failure( Error.Conflict( "ATTEMPT_NOT_FINISHED", "Results are not available while the attempt is in progress.")); } 
            // 3. Get answers
            var answers = await _mediator.Send( new GetAttemptAnswersQuery( request.AttemptId), cancellationToken); 
            // 4. Build response
            var response = new GetAttemptResultsResponse
            {
                AttemptId = attempt.AttemptId, QuizId = attempt.QuizId, QuizTitle = attempt.QuizTitle, Status = attempt.Status, StartTime = attempt.StartTime,
                SubmittedAt = attempt.SubmittedAt, Score = attempt.Score, Passed = attempt.Passed, Questions = answers 
            }; 
            return Result<GetAttemptResultsResponse>.Success(response); 
        } 
    }
}
