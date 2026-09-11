using exam_system.Features.Identity.VerifyEmailOtp.DTOs.Response;
using exam_system.Features.Identity.VerifyEmailOtp.Orchestrators;
using exam_system.Features.Shared;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Identity.VerifyEmailOtp.Controllers;

[ApiController]
[Route("api/identity")]
public class VerifyEmailOtpController(IMediator mediator) : ControllerBase
{
    [HttpPost("verify-otp")]
    public async Task<ActionResult<ApiResponse<VerifyEmailOtpResponse>>> Verify(
        [FromBody] VerifyEmailOtpOrchestratorRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        var response = result.ToApiResponse();
        return StatusCode(response.StatusCode, response);
    }
}