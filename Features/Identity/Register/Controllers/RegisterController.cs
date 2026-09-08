using exam_system.Features.Identity.Register.Commands;
using exam_system.Features.Shared;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Identity.Register.Controllers;

[ApiController]
[Route("api/identity/[controller]")]
public class RegisterController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        var response = result.ToApiResponse();  
        
        return StatusCode(response.StatusCode, response);
    }
}