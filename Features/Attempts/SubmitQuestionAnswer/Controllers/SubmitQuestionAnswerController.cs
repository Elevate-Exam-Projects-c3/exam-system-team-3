using exam_system.Features.Attempts.SubmitQuestionAnswer.Orchestrators;
using exam_system.Features.Attempts.SubmitQuestionAnswer.ViewModels;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Controllers
{
    [ApiController]
    [Route("api/admin/quizzes")]
    [Authorize(Roles = "Student")]
    public class SubmitQuestionAnswerController(IMediator _mediator) : ControllerBase
    {
        
        [HttpPut("{attemptId:guid}/questions/{questionId:guid}/answer")]

        public async Task<IActionResult> SubmitQuestionAnswer(Guid attemptId, Guid questionId, [FromBody] SubmitQuestionAnswerRequest request, CancellationToken cancellationToken)
        {
            var studentId = GetStudentId(); 
            var result = await _mediator.Send(new SubmitQuestionAnswerOrchestrator(attemptId, questionId, studentId, request), cancellationToken);
            if (!result.IsSuccess) { return BadRequest(result); }
            return Ok(result.Value);
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
