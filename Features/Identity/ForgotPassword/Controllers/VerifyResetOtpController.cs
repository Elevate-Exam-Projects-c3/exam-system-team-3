using exam_system.Features.Identity.ForgotPassword.DTOs.Request;
using exam_system.Features.Identity.ForgotPassword.DTOs.Response;
using exam_system.Features.Identity.ForgotPassword.Orchestrators;

namespace exam_system.Features.Identity.ForgotPassword.Controllers;

[ApiController]
[Route("api/identity")]
[Tags("Identity - Password Reset")]
public class VerifyResetOtpController(IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("verify-reset-otp")]
    public async Task<ActionResult<ApiResponse<VerifyResetOtpResponse>>> Verify(
        [FromBody] VerifyResetOtpRequest request, CancellationToken cancellationToken)
    {
        var orchestrator = new VerifyResetOtpOrchestrator(request.Email, request.Otp);
        var result = await mediator.Send(orchestrator, cancellationToken);
        var response = result.ToApiResponse();
        return StatusCode(response.StatusCode, response);
    }
}