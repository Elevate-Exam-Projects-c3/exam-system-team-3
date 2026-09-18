using exam_system.Features.Attempts.SubmitAttempt.Orchestrators;

namespace exam_system.Features.Attempts.SubmitAttempt.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/attempts")]
    public class SubmitQuizAttemptController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("{attemptId:guid}/submit")]
        public async Task<IActionResult> SubmitQuizAttempt(Guid attemptId,CancellationToken cancellationToken)
        {
            var studentId = GetStudentId();

            var result = await _mediator.Send(new SubmitQuizAttemptOrchestrator(
                    attemptId,
                    studentId),
                cancellationToken);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Value);
        }

        private Guid GetStudentId()
        {
            var studentId = User.FindFirst("sub")?.Value;

            if (!Guid.TryParse(studentId, out var id))
            {
                throw new UnauthorizedAccessException(
                    "Student ID was not found in the authentication token.");
            }

            return id;
        }
    }
}
