using exam_system.Features.Questions.AdminUpdateQuestion.Orchestrators;
using exam_system.Features.Questions.AdminUpdateQuestion.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Questions.AdminUpdateQuestion.Controllers
{
    [ApiController]
    [Route(
    "api/admin/quizzes/{quizId:guid}/questions/{questionId:guid}/options")]
    public class AdminQuestionOptionsController(IMediator _mediator): ControllerBase
    {
        [HttpPut("{optionId:guid}")]
        public async Task<IActionResult> Update(Guid quizId,Guid questionId,Guid optionId,[FromBody] UpdateQuestionOptionRequest request,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new UpdateQuestionOptionOrchestrator(
                    quizId,
                    questionId,
                    optionId,
                    request),
                cancellationToken);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Value);
        }
    }
}
