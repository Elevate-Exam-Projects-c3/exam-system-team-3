using exam_system.Features.Diplomas.GetStudentDashboard.DTOs.Response;
using exam_system.Features.Diplomas.GetStudentDashboard.Orchestrators;
using exam_system.Features.Shared;
using exam_system.Features.Shared.Interfaces;
using exam_system.Features.Shared.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Diplomas.GetStudentDashboard.Controllers;

[ApiController]
[Route("api/student")]
[Authorize(Roles = nameof(UserRole.Student))]
public class StudentDashboardController(IMediator mediator, ICurrentUser currentUser) : ControllerBase
{
    [HttpGet("dashboard")]
    public async Task<ActionResult<ApiResponse<StudentDashboardResponse>>> GetDashboard(
        CancellationToken cancellationToken)
    {
        if (currentUser.UserId is not { } userId)
        {
            var unauthorized = ApiResponse<StudentDashboardResponse>.Fail(
                "Authentication required.", StatusCodes.Status401Unauthorized);
            return StatusCode(unauthorized.StatusCode, unauthorized);
        }

        var result = await mediator.Send(new StudentDashboardOrchestrator(userId), cancellationToken);
        var response = result.ToApiResponse();

        return StatusCode(response.StatusCode, response);
    }
}