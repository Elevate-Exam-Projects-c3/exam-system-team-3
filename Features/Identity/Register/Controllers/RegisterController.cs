
using exam_system.Features.Identity.Register.DTOs.Request;
using exam_system.Features.Identity.Register.DTOs.Response;
using exam_system.Features.Identity.Register.Orchestrators;
using exam_system.Features.Shared;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Identity.Register.Controllers;

[ApiController]
[Route("api/identity")]
public class RegisterController(IMediator mediator ) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<RegisterUserResponse>>> Register(
        [FromBody] RegisterOrchestratorRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        var response = result.ToApiResponse();  
        
        return StatusCode(response.StatusCode, response);
    }
}