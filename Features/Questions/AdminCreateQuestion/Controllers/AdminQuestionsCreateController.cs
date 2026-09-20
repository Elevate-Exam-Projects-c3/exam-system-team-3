using exam_system.Features.Questions.AdminCreateQuestion.Orchestrators;
using exam_system.Features.Questions.AdminCreateQuestion.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Questions.AdminCreateQuestion.Controllers
{
    
    [ApiController]
    [Route("api/admin/Questions")]
    public class AdminQuestionsCreateController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Create(Guid quizId,[FromBody] CreateQuestionRequest request,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateQuestionOrchestrator(
                    quizId,
                    request),
                cancellationToken);

            var apiResponse = result.ToApiResponse();

            return StatusCode(apiResponse.StatusCode, apiResponse);

            
        }
    }
}
