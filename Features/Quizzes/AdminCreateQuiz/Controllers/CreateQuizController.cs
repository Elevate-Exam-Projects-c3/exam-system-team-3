using exam_system.Features.Quizzes.AdminCreateQuiz.Commands;
using exam_system.Features.Quizzes.AdminCreateQuiz.Orchestrators;
using exam_system.Features.Quizzes.AdminCreateQuiz.ViewModel;
using exam_system.Features.Shared;
using exam_system.Features.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Quizzes.AdminCreateQuiz.Controllers
{
    [ApiController]
    [Route("api/admin/quizzes")]
    [Authorize(Roles = "Admin")]
    public class CreateQuizController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> CreateQuiz([FromBody] CreateQuizRequest request,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateQuizOrchestrator(request),cancellationToken);

            var apiResponse = result.ToApiResponse();

            return StatusCode(apiResponse.StatusCode, apiResponse);
        }
    }
}
