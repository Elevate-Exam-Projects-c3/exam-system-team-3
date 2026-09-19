using exam_system.Features.Questions.AdminUpdateQuestion.Orchestrators;
using exam_system.Features.Questions.AdminUpdateQuestion.ViewModels;

namespace exam_system.Features.Questions.AdminUpdateQuestion.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route(
    "api/admin/quizzes/{quizId:guid}/questions/{questionId:guid}/options")]
    public class AdminQuestionOptionsController(IMediator _mediator)
    : ControllerBase
    {
        [HttpPut("{optionId:guid}")]
        public async Task<ActionResult> Update(Guid quizId,Guid questionId,Guid optionId,[FromBody] UpdateQuestionOptionRequest request,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateQuestionOptionOrchestrator(
                    quizId,
                    questionId,
                    optionId,
                    request),
                cancellationToken);

            var apiResponse = result.ToApiResponse();

            return StatusCode(apiResponse.StatusCode, apiResponse);
        }
    }
}
