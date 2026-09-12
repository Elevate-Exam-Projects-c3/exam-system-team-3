using exam_system.Features.Identity.VerifyEmailOtp.DTOs.Request;
using exam_system.Features.Identity.VerifyEmailOtp.DTOs.Response;
using exam_system.Features.Identity.VerifyEmailOtp.Orchestrators;
using exam_system.Features.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Identity.VerifyEmailOtp.Controllers;

[ApiController]
[Route("api/identity")]
public class VerifyEmailOtpController(IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("verify-otp")]
    public async Task<ActionResult<ApiResponse<VerifyEmailOtpResponse>>> Verify(
        [FromBody] VerifyEmailOtpRequest request,
        CancellationToken cancellationToken)
    {
        
        var orchestrator = new VerifyEmailOtpOrchestrator(
            request.Email,
            request.Otp);
        
        var result = await mediator.Send(
            orchestrator, cancellationToken);
        var response = result.ToApiResponse();
        return StatusCode(response.StatusCode, response);
    }
}