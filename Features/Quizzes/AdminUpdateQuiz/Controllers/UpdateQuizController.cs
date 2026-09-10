using exam_system.Features.Quizzes.AdminUpdateQuiz.DTO;
using exam_system.Features.Quizzes.AdminUpdateQuiz.Orchestrators;
using exam_system.Features.Shared;
using exam_system.Features.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Quizzes.AdminUpdateQuiz.Controllers
{
    [ApiController]
    [Route("api/admin/quizzes")]
    public class UpdateQuizController(UpdateQuizOrchestrator _updateQuizOrchestrator) : ControllerBase
    {
        [HttpPut("{quizId:guid}")]
        public async Task<IActionResult> UpdateQuiz(Guid quizId,[FromBody] UpdateQuizRequest request,CancellationToken cancellationToken)
        {
            var result = await _updateQuizOrchestrator.ExecuteAsync(quizId,request,cancellationToken);

            if (!result.IsSuccess)
            {
                var error = result.Errors.First();

                return error.Type switch
                {
                    ErrorKind.NotFound => NotFound(error),
                    ErrorKind.Conflict => Conflict(error),
                    ErrorKind.Validation => BadRequest(error),
                    ErrorKind.Unauthorized => Unauthorized(error),
                    ErrorKind.Forbidden => StatusCode(403, error),
                    _ => StatusCode(500, error)
                };
            }

            return Ok(result.Value);
        }
    }
}

