
using exam_system.Features.Identity.Register.DTOs.Request;
using exam_system.Features.Identity.Register.DTOs.Response;
using exam_system.Features.Identity.Register.Orchestrators;


namespace exam_system.Features.Identity.Register.Controllers;

[ApiController]
[Route("api/identity")]
[Tags("Identity")]
public class RegisterController(IMediator mediator ) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<RegisterUserResponse>>> Register(
        [FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var orchestrator = new RegisterOrchestrator
            (request.FullName, request.Email,request.Password);
        
        var result = await mediator.Send(orchestrator, cancellationToken);
        var response = result.ToApiResponse( StatusCodes.Status201Created);  
        
        return StatusCode(response.StatusCode, response);
    }
}