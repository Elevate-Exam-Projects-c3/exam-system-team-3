using exam_system.Features.Quizzes.AdminPublishQuiz.Orchestrator;
using exam_system.Features.Quizzes.AdminQuizPublishCheck.Commands;
using exam_system.Features.Quizzes.AdminQuizPublishCheck.DTOs;
using exam_system.Features.Quizzes.AdminUnpublishQuiz.Orchestrator;

namespace exam_system.Features.Quizzes.AdminPublishCheck.Controllers;

[ApiController]
[Route("api/admin/quizzes")]
[Authorize(Roles = "Admin")]
    
public sealed class AdminPublishCheckController(ISender sender) : ControllerBase
{
    [HttpGet("{id:guid}/publish-check")]
    public async Task<ActionResult<ApiResponse<PublishCheckResponse>>> PublishCheck(Guid id,CancellationToken cancellationToken)
    {
        var result = await sender.Send( new PublishCheckOrchestrator(id),cancellationToken);
        var apiResponse = result.ToApiResponse();
        return StatusCode(apiResponse.StatusCode, apiResponse);
    }

    [HttpPatch("{id:guid}/publish")]
    public async Task<ActionResult<ApiResponse<Updated>>> Publish(Guid id,CancellationToken cancellationToken)
    {
        var result = await sender.Send(new PublishQuizOrchestrator(id),cancellationToken);
        var apiResponse = result.ToApiResponse();
        return StatusCode(apiResponse.StatusCode, apiResponse);
    }

    [HttpPatch("{id:guid}/unpublish")]
    public async Task<ActionResult<ApiResponse<Updated>>> Unpublish( Guid id,CancellationToken cancellationToken)
    {
        var result = await sender.Send(new UnpublishQuizOrchestrator(id),cancellationToken);
        var apiResponse = result.ToApiResponse();
        return StatusCode(apiResponse.StatusCode, apiResponse);
    }
}