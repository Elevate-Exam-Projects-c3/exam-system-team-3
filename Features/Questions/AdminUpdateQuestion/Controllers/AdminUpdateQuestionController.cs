using exam_system.Features.Questions.AdminUpdateQuestion.Orchestrators;
using exam_system.Features.Questions.AdminUpdateQuestion.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Questions.AdminUpdateQuestion.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/quizzes/{quizId:guid}/questions")]
    public class AdminUpdateQuestionController(IMediator _mediator): ControllerBase
    {
        [HttpPut("{questionId:guid}")]
        public async Task<ActionResult> Update(Guid quizId,Guid questionId,[FromBody] UpdateQuestionRequest request,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateQuestionOrchestrator(
                    quizId,
                    questionId,
                    request),
                cancellationToken);

            var apiResponse = result.ToApiResponse();

            return StatusCode(apiResponse.StatusCode, apiResponse);
        }
    }
}
