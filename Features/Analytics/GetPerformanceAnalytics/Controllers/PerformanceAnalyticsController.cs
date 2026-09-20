using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs.Response;
using exam_system.Features.Analytics.GetPerformanceAnalytics.Orchestrators;


namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Controllers;

[ApiController]
[Route("api/admin/analytics")]
[Authorize(Roles = nameof(UserRole.Admin))]
public class PerformanceAnalyticsController(IMediator mediator) : ControllerBase
{
    [HttpGet("performance")]
    public async Task<ActionResult<ApiResponse<PerformanceAnalyticsResponse>>> GetPerformance(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] Guid? diplomaId,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new PerformanceAnalyticsOrchestrator(dateFrom, dateTo, diplomaId),
            cancellationToken);

        var response = result.ToApiResponse();
        return StatusCode(response.StatusCode, response);
    }
}