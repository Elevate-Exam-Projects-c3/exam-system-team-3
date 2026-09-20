using exam_system.Features.Attempts.SubmitQuestionAnswer.Orchestrators;
using exam_system.Features.Attempts.SubmitQuestionAnswer.ViewModels;
using exam_system.Features.Shared.Interfaces;
using exam_system.Features.Shared.Queries;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Controllers
{
    [ApiController]
    [Route("api/admin/quizzes")]
    [Authorize(Roles = "Student")]
    public class SubmitQuestionAnswerController(IMediator _mediator, ICurrentUser currentUser) : ControllerBase
    {
        
        [HttpPut("{attemptId:guid}/questions/{questionId:guid}/answer")]

        public async Task<ActionResult<Result<SubmitQuestionAnswerResponse>>> SubmitQuestionAnswer(Guid attemptId, Guid questionId, [FromBody] SubmitQuestionAnswerRequest request, CancellationToken cancellationToken)
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

            var result = await _mediator.Send(new SubmitQuestionAnswerOrchestrator(attemptId, questionId, studentId.Value, request), cancellationToken);

            var apiResponse = result.ToApiResponse();

            return StatusCode(apiResponse.StatusCode, apiResponse);
        }
        
    }
}
