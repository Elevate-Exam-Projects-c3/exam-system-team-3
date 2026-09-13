using exam_system.Features.Attempts.StartAttempt.Orchestrators;

namespace exam_system.Features.Attempts.StartAttempt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizAttemptsController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("quizzes/{quizId:guid}/attempts")]
        public async Task<IActionResult> StartAttempt(Guid quizId,CancellationToken cancellationToken)
        {
            var studentId = GetStudentId();

            var result = await _mediator.Send(
                new StartAttemptOrchestrator(
                    quizId,
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new UnauthorizedAccessException(
                    "Student identity was not found.");
            }

            return Guid.Parse(userId);
        }
    }
}
