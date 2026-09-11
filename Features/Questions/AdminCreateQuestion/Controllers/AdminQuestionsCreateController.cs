using exam_system.Features.Questions.AdminCreateQuestion.Orchestrators;
using exam_system.Features.Questions.AdminCreateQuestion.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Questions.AdminCreateQuestion.Controllers
{
    
    [ApiController]
    [Route("api/admin/quizzes/{quizId:guid}/questions")]
    public class AdminQuestionsCreateController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(Guid quizId,[FromBody] CreateQuestionRequest request,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateQuestionOrchestrator(
                    quizId,
                    request),
                cancellationToken);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Created(
                $"/api/admin/quizzes/{quizId}/questions/{result.Value}",
                result.Value);
        }
    }
}
