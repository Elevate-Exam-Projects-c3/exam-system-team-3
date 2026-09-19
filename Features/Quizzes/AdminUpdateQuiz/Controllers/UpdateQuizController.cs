using exam_system.Features.Quizzes.AdminUpdateQuiz.DTO;
using exam_system.Features.Quizzes.AdminUpdateQuiz.Orchestrators;
using exam_system.Features.Shared;
using exam_system.Features.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Quizzes.AdminUpdateQuiz.Controllers
{
    [ApiController]
    [Route("api/admin/quizzes")]
    [Authorize(Roles = "Admin")]
    public class UpdateQuizController(IMediator _mediator) : ControllerBase
    {
        [HttpPut("{quizId:guid}")]
        public async Task<ActionResult> UpdateQuiz(Guid quizId,[FromBody] UpdateQuizRequest request,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateQuizOrchestrator(quizId,request),cancellationToken);

            var apiResponse = result.ToApiResponse();

            return StatusCode(apiResponse.StatusCode, apiResponse);
        }
    }
}

