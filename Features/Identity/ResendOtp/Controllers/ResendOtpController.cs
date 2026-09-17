using exam_system.Features.Identity.ResendOtp.DTOs.Request;
using exam_system.Features.Identity.ResendOtp.DTOs.Response;
using exam_system.Features.Identity.ResendOtp.Orchestrators;
using exam_system.Features.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Identity.ResendOtp.Controllers;

[ApiController]
[Route("api/identity")]

[Tags("Identity")]
public class ResendOtpController(IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("resend-otp")]
    public async Task<ActionResult<ApiResponse<ResendOtpResponse>>> Resend(
        [FromBody] ResendOtpRequest request,
        CancellationToken cancellationToken)
    {
        var orchestrator = new ResendOtpOrchestrator(request.Email);

        var result = await mediator.Send(orchestrator, cancellationToken);
        var response = result.ToApiResponse();

        return StatusCode(response.StatusCode, response);
    }
}