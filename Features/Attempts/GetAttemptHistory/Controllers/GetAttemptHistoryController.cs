using exam_system.Features.Attempts.GetAttemptHistory.Queries;
using exam_system.Features.Attempts.GetAttemptHistory.ViewModels;
using exam_system.Features.Diplomas.AdminCreateDiploma.DTOS;

namespace exam_system.Features.Attempts.GetAttemptHistory.Controllers
{
    [Authorize(Roles = "Student")]
    [ApiController]
    [Route("api/attempts")]
    public class GetAttemptHistoryController(IMediator _mediator) : ControllerBase 
    {         
        [HttpGet("history")]
        public async Task<ActionResult<ApiResponse<PaginatedResult<AttemptHistoryItemViewModel>>>> GetAttemptHistory([FromQuery] int pageIndex = 1,
                                                        [FromQuery] int pageSize = 10,
                                                        CancellationToken cancellationToken = default) 
        {
            var studentId = GetStudentId(); 
            var result = await _mediator.Send(new GetAttemptHistoryQuery(studentId, pageIndex, pageSize), cancellationToken);
            var apiResponse = result.ToApiResponse();
            return StatusCode(apiResponse.StatusCode, apiResponse);
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
