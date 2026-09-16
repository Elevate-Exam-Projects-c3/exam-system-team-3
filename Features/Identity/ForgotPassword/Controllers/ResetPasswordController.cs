using exam_system.Features.Identity.ForgotPassword.DTOs.Request;
using exam_system.Features.Identity.ForgotPassword.DTOs.Response;
using exam_system.Features.Identity.ForgotPassword.Orchestrators;

namespace exam_system.Features.Identity.ForgotPassword.Controllers;

[ApiController]
[Route("api/identity")]
[Tags("Identity - Password Reset")]
public class ResetPasswordController(IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("reset-password")]
    public async Task<ActionResult<ApiResponse<ResetPasswordResponse>>> Reset(
        [FromBody] ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        var orchestrator = new ResetPasswordOrchestrator(
            request.Email, request.ResetToken, request.NewPassword, request.ConfirmPassword);

        var result = await mediator.Send(orchestrator, cancellationToken);
        var response = result.ToApiResponse();
        return StatusCode(response.StatusCode, response);
    }
}