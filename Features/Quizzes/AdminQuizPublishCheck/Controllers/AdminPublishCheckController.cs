using exam_system.Features.Quizzes.AdminQuizPublishCheck.Commands;
using exam_system.Features.Quizzes.AdminQuizPublishCheck.DTOs;

namespace exam_system.Features.Quizzes.AdminPublishCheck.Controllers;

[ApiController]
[Route("api/admin/quizzes")]
[Authorize(Roles = "Admin")]
    
public sealed class AdminPublishCheckController(ISender sender) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("{id:guid}/publish-check")]
    public async Task<ActionResult<ApiResponse<PublishCheckResponse>>> PublishCheck(Guid id,CancellationToken cancellationToken)
    {
        var result = await sender.Send( new PublishCheckOrchestrator(id),cancellationToken);
        var apiResponse = result.ToApiResponse();
        return StatusCode(apiResponse.StatusCode, apiResponse);
    }
}