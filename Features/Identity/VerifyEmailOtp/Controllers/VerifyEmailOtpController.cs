using exam_system.Features.Identity.VerifyEmailOtp.Commands;
using exam_system.Features.Shared;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Identity.VerifyEmailOtp.Controllers;

[ApiController]
[Route("api/identity/[controller]")]

public class VerifyEmailOtpController(IMediator mediator):ControllerBase
{
    
    [HttpPost]
    
    public async Task<IActionResult> Verify(
        [FromBody] VerifyEmailOtpCommand command,
    CancellationToken  cancellationToken)
    {
        
        var result = await mediator.Send(command, cancellationToken);
        var response = result.ToApiResponse();
        return StatusCode(response.StatusCode, response);

    }
}
