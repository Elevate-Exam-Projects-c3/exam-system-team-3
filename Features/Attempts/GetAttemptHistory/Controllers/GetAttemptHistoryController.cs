using exam_system.Features.Attempts.GetAttemptHistory.Queries;
using exam_system.Features.Attempts.GetAttemptHistory.ViewModels;
using exam_system.Features.Shared.Interfaces;
using exam_system.Features.Shared.Queries;

namespace exam_system.Features.Attempts.GetAttemptHistory.Controllers
{
    [Authorize(Roles = "Student")]
    [ApiController]
    [Route("api/attempts")]
    public class GetAttemptHistoryController(IMediator mediator, ICurrentUser currentUser) : ControllerBase
    {
        [HttpGet("history")]
        public async Task<ActionResult<ApiResponse<PaginatedResult<AttemptHistoryItemViewModel>>>> GetAttemptHistory([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            if (!currentUser.UserId.HasValue)
            {
                throw new UnauthorizedAccessException(
                    "User ID was not found in the authentication token.");
            }

            var studentId = await mediator.Send(new GetStudentIdByUserIdQuery(currentUser.UserId.Value), cancellationToken);

            if (!studentId.HasValue)
            {
                throw new UnauthorizedAccessException(
                    "Student was not found for the authenticated user.");
            }

            var result = await mediator.Send(new GetAttemptHistoryQuery(
                    studentId.Value,
                    pageIndex,
                    pageSize),
                cancellationToken);

            var apiResponse = result.ToApiResponse();

            return StatusCode(apiResponse.StatusCode, apiResponse);
        }
    }
}
