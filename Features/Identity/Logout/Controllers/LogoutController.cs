using exam_system.Features.Identity.Logout.Commands;
using exam_system.Features.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Identity.Logout.Controllers;

[ApiController]
[Route("api/identity")]
public class LogoutController(IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("logout")]
    public async Task<ActionResult<ApiResponse<object>>> Logout(CancellationToken cancellationToken)
    {
        var rawToken = Request.Cookies["refreshToken"];

        if (!string.IsNullOrEmpty(rawToken))
        {
            await mediator.Send(new RevokeRefreshTokenCommand(rawToken), cancellationToken);
        }

        Response.Cookies.Delete("refreshToken");

        var result = Result<object>.Success(new { });

        var response = result.ToApiResponse();

        return StatusCode(response.StatusCode, response);
    }
}