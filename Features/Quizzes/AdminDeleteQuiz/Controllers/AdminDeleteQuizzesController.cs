using exam_system.Features.Quizzes.AdminDeleteQuiz.Orchestrators;
using exam_system.Features.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Quizzes.AdminDeleteQuiz.Controllers
{
    [ApiController]
    [Route("api/admin/quizzes")]
    public class AdminDeleteQuizzesController(IMediator _mediator) : ControllerBase
    {
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteQuizOrchestrator(id),cancellationToken);

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
            return StatusCode(StatusCodes.Status201Created, result.Value);
        }
    }
}
}
