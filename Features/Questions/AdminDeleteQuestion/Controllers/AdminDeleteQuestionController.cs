using exam_system.Features.Questions.AdminDeleteQuestion.Orchestrators;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Questions.AdminDeleteQuestion.Controllers
{
    [ApiController]
    [Route("api/admin/Questions")]
    [Authorize(Roles = "Admin")]
    public class AdminDeleteQuestionController(IMediator _mediator) : ControllerBase
    {
        [HttpDelete("{questionId:guid}")]
        public async Task<IActionResult> Delete(Guid quizId,Guid questionId,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteQuestionOrchestrator(
                    quizId,
                    questionId),cancellationToken);

            if (!result.IsSuccess)
            {
                return Conflict(result);
            }

            return NoContent();
        }
    }
}
