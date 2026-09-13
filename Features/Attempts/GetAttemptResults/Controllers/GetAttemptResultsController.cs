using exam_system.Features.Attempts.GetAttemptResults.Orchestrators;

namespace exam_system.Features.Attempts.GetAttemptResults.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/attempts")]
    public class GetAttemptResultsController(IMediator _mediator) : ControllerBase 
    {
        
        [HttpGet("{attemptId:guid}/results")] 
        public async Task<IActionResult> GetAttemptResults(Guid attemptId, CancellationToken cancellationToken)
        { 
            var studentId = GetStudentId(); 
            var result = await _mediator.Send(new GetAttemptResultsOrchestrator(attemptId, studentId), cancellationToken); 
            if (!result.IsSuccess) 
            {
                return BadRequest(result); } 
            return Ok(result.Value); }
        private Guid GetStudentId()
        {
            var studentId = User.FindFirst("sub")?.Value; 
            if (!Guid.TryParse(studentId, out var id))
            {
                throw new UnauthorizedAccessException("Student ID was not found in the authentication token.");
            } 
            return id; 
        }
    }
}
