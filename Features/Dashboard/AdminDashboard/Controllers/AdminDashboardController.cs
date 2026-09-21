using exam_system.Features.Dashboard.AdminDashboard.DTOs;
using exam_system.Features.Dashboard.AdminDashboard.Orchestrators;
using exam_system.Features.Dashboard.AdminDashboard.Queries;

namespace exam_system.Features.Dashboard.AdminDashboard.Controllers;

[ApiController]
[Route("api/admin/dashboard")]
[Authorize(Roles = "Admin")]
public sealed class AdminDashboardController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<AdminDashboardResponse>>> GetDashboard(CancellationToken cancellationToken)
    {
        var result = await sender.Send( new AdminDashboardOrchestrator(),cancellationToken);
        var apiResponse = result.ToApiResponse();
        return StatusCode(apiResponse.StatusCode, apiResponse);
    }
}