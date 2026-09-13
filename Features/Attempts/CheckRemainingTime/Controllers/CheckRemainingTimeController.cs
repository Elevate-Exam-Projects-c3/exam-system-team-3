using exam_system.Features.Attempts.CheckRemainingTime.Orchestrators;
using exam_system.Features.Attempts.CheckRemainingTime.Queries;

namespace exam_system.Features.Attempts.CheckRemainingTime.Controllers
{
    [ApiController]
    [Route("api/attempts")]
    public class CheckRemainingTimeController(
    IMediator _mediator)
    : ControllerBase
    {
        [HttpGet("{attemptId:guid}/remaining-time")]
        public async Task<IActionResult> CheckRemainingTime(Guid attemptId,CancellationToken cancellationToken)
        {
            var studentId = GetStudentId();

            var result = await _mediator.Send(new CheckRemainingTimeOrchestrator(
                    attemptId,
                    studentId),
                cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result.Value);
        }

        private Guid GetStudentId()
        {
            // Your existing JWT student-id implementation
            return Guid.Parse(User.FindFirst("sub")!.Value);
        }
    }
}
