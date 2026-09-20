using exam_system.Features.Quizzes.AdminDeleteQuiz.Orchestrators;
using exam_system.Features.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Quizzes.AdminDeleteQuiz.Controllers
{
    [ApiController]
    [Route("api/admin/quizzes")]
    [Authorize(Roles = "Admin")]
    public class AdminDeleteQuizzesController(IMediator _mediator) : ControllerBase
    {
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteQuizOrchestrator(id),cancellationToken);

            var apiResponse = result.ToApiResponse();

            return StatusCode(apiResponse.StatusCode, apiResponse);
        }
    }
}

