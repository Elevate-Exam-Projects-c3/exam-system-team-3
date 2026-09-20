using exam_system.Features.Attempts.SubmitAttempt.Orchestrators;
using exam_system.Features.Attempts.SubmitAttempt.ViewModels;
using exam_system.Features.Shared.Interfaces;
using exam_system.Features.Shared.Queries;

namespace exam_system.Features.Attempts.SubmitAttempt.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/attempts")]
    public class SubmitQuizAttemptController(IMediator _mediator, ICurrentUser currentUser) : ControllerBase
    {
        [HttpPost("{attemptId:guid}/submit")]
        public async Task<ActionResult<Result<SubmitQuizAttemptResponse>>> SubmitQuizAttempt(Guid attemptId,CancellationToken cancellationToken)
        {
            if (!currentUser.UserId.HasValue)
            {
                throw new UnauthorizedAccessException(
                    "User ID was not found in the authentication token.");
            }

            var studentId = await _mediator.Send(new GetStudentIdByUserIdQuery(currentUser.UserId.Value), cancellationToken);

            if (!studentId.HasValue)
            {
                throw new UnauthorizedAccessException(
                    "Student was not found for the authenticated user.");
            }

            var result = await _mediator.Send(new SubmitQuizAttemptOrchestrator(
                    attemptId,
                    studentId.Value),
                cancellationToken);

            var apiResponse = result.ToApiResponse();

            return StatusCode(apiResponse.StatusCode, apiResponse);
        }

        
    }
}
