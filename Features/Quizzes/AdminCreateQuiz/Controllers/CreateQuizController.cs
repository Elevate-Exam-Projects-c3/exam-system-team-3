using exam_system.Features.Quizzes.AdminCreateQuiz.Commands;
using exam_system.Features.Quizzes.AdminCreateQuiz.Orchestrators;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Quizzes.AdminCreateQuiz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreateQuizController(CreateQuizOrchestrator _orchestrator) : ControllerBase
    {
        
        [HttpPost]
        public async Task<IActionResult> Create(CreateQuizCommand command,CancellationToken cancellationToken)
        {
            var quizId = await _orchestrator.ExecuteAsync(command,cancellationToken);

            return Ok(quizId);
        }
    }
}
