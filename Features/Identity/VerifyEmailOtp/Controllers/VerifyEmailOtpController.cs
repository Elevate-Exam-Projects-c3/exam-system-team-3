using exam_system.Features.Identity.VerifyEmailOtp.DTOs.Request;
using exam_system.Features.Identity.VerifyEmailOtp.DTOs.Response;
using exam_system.Features.Identity.VerifyEmailOtp.Orchestrators;
using exam_system.Features.Shared;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Identity.VerifyEmailOtp.Controllers;

[ApiController]
[Route("api/identity")]
public class VerifyEmailOtpController(VerifyEmailOtpOrchestrator orchestrator) : ControllerBase
{
    [HttpPost("verify-otp")]
    public async Task<ActionResult<ApiResponse<VerifyEmailOtpResponse>>> Verify(
        [FromBody] VerifyEmailOtpRequest request,
        CancellationToken cancellationToken)
    {
        var result = await orchestrator.VerifyAsync(request, cancellationToken);
        var response = result.ToApiResponse();
        return StatusCode(response.StatusCode, response);
    }
}