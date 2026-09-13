using exam_system.Features.Attempts.GetAttemptHistory.Queries;

namespace exam_system.Features.Attempts.GetAttemptHistory.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/attempts")]
    public class GetAttemptHistoryController(IMediator _mediator) : ControllerBase 
    {         
        [HttpGet("history")]
        public async Task<IActionResult> GetAttemptHistory(CancellationToken cancellationToken) 
        {
            var studentId = GetStudentId(); 
            var result = await _mediator.Send(new GetAttemptHistoryQuery(studentId), cancellationToken); 
            return Ok(result);
        }
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
