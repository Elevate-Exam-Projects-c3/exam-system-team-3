using exam_system.Features.Identity.ForgotPassword.DTOs.Request;
using exam_system.Features.Identity.ForgotPassword.DTOs.Response;
using exam_system.Features.Identity.ForgotPassword.Orchestrators;

namespace exam_system.Features.Identity.ForgotPassword.Controllers;

[ApiController]
[Route("api/identity")]
[Tags("Identity - Password Reset")]
public class ForgotPasswordController(IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("forgot-password")]
    public async Task<ActionResult<ApiResponse<ForgotPasswordResponse>>> ForgotPassword(
        [FromBody] ForgotPasswordRequest request,
        CancellationToken cancellationToken)
    {
        var orchestrator = new ForgotPasswordOrchestrator(request.Email);
        var result = await mediator.Send(orchestrator, cancellationToken);
        var response = result.ToApiResponse();
        return StatusCode(response.StatusCode, response);
    }
}