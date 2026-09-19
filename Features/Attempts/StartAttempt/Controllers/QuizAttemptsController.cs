using exam_system.Features.Attempts.StartAttempt.Orchestrators;
using exam_system.Features.Shared.Interfaces;
using exam_system.Features.Shared.Queries;

namespace exam_system.Features.Attempts.StartAttempt.Controllers
{
    [ApiController]
    [Route("api/admin/quizzes")]
    [Authorize]
    public class QuizAttemptsController(IMediator _mediator, ICurrentUser currentUser) : ControllerBase
    {
        [HttpPost("quizzes/{quizId:guid}/attempts")]
        public async Task<ActionResult> StartAttempt(Guid quizId,CancellationToken cancellationToken)
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

            var result = await _mediator.Send(
                new StartAttemptOrchestrator(
                    quizId,
                    studentId.Value),
                cancellationToken);

            var apiResponse = result.ToApiResponse();

            return StatusCode(apiResponse.StatusCode, apiResponse);
        }

        
    }
}
