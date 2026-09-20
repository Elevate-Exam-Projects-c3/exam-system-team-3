using exam_system.Features.Attempts.CheckRemainingTime.DTOs;
using exam_system.Features.Attempts.CheckRemainingTime.Orchestrators;
using exam_system.Features.Attempts.CheckRemainingTime.Queries;
using exam_system.Features.Shared.Interfaces;
using exam_system.Features.Shared.Queries;

namespace exam_system.Features.Attempts.CheckRemainingTime.Controllers
{
    [ApiController]
    [Route("api/admin/quizzes")]
    [Authorize(Roles = "Student")]
    public class CheckRemainingTimeController(IMediator _mediator, ICurrentUser currentUser) : ControllerBase
    {
        [HttpGet("{attemptId:guid}/remaining-time")]
        public async Task<ActionResult<ApiResponse<RemainingTimeResponse>>> CheckRemainingTime(Guid attemptId,CancellationToken cancellationToken)
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

            var result = await _mediator.Send(new CheckRemainingTimeOrchestrator(
                    attemptId,
                    studentId.Value),
                cancellationToken);

            var apiResponse = result.ToApiResponse();

            return StatusCode(apiResponse.StatusCode, apiResponse);
        }

       
    }
}
