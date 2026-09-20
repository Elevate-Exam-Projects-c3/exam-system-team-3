using exam_system.Features.Attempts.GetAttemptResults.Orchestrators;
using exam_system.Features.Attempts.GetAttemptResults.ViewModels;
using exam_system.Features.Shared.Interfaces;
using exam_system.Features.Shared.Queries;

namespace exam_system.Features.Attempts.GetAttemptResults.Controllers
{
    [Authorize(Roles = "Student")]
    [ApiController]
    [Route("api/attempts")]
    public class GetAttemptResultsController(IMediator _mediator, ICurrentUser currentUser) : ControllerBase 
    {
        
        [HttpGet("{attemptId:guid}/results")] 
        public async Task<ActionResult<Result<GetAttemptResultsResponse>>> GetAttemptResults(Guid attemptId, CancellationToken cancellationToken)
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

            var result = await _mediator.Send(new GetAttemptResultsOrchestrator(attemptId, studentId.Value), cancellationToken);

            var apiResponse = result.ToApiResponse();

            return StatusCode(apiResponse.StatusCode, apiResponse);

        }
        
    }
}
