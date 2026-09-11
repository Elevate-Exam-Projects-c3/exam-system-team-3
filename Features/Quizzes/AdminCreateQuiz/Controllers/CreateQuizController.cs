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
    public class CreateQuizController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateQuiz(
            [FromBody] CreateQuizRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new CreateQuizOrchestrator(request),
                cancellationToken);

            if (!result.IsSuccess)
            {
                var error = result.Errors.First();

                return error.Type switch
                {
                    ErrorKind.Conflict => Conflict(error),
                    ErrorKind.Validation => BadRequest(error),
                    ErrorKind.NotFound => NotFound(error),
                    ErrorKind.Unauthorized => Unauthorized(error),
                    ErrorKind.Forbidden =>
                        StatusCode(StatusCodes.Status403Forbidden, error),
                    _ =>
                        StatusCode(
                            StatusCodes.Status500InternalServerError,
                            error)
                };
            }

            return StatusCode(StatusCodes.Status201Created,result.Value);
        }
    }
}
