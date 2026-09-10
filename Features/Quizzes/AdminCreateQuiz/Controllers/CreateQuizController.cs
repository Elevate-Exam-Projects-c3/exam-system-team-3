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
    public class CreateQuizController(CreateQuizOrchestrator _createQuizOrchestrator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateQuiz([FromBody] CreateQuizRequest request,CancellationToken cancellationToken)
        {
            var result = await _createQuizOrchestrator.ExecuteAsync(request,cancellationToken);

            if (!result.IsSuccess)
            {
                var error = result.Errors.First();

                return error.Type switch
                {
                    ErrorKind.Conflict => Conflict(error),
                    ErrorKind.NotFound => NotFound(error),
                    ErrorKind.Validation => BadRequest(error),
                    _ => StatusCode(500, error)
                };
            }

            return Ok(result.Value);
        }
    }
}
