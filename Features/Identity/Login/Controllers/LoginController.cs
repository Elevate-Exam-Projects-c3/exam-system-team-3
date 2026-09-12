using exam_system.Common.Auth.RefreshToken;
using exam_system.Features.Identity.Login.DTOs.Request;
using exam_system.Features.Identity.Login.Orchestrators;
using exam_system.Features.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Identity.Login.Controllers;
[ApiController]
[Route("api/identity")]
public class LoginController(
    IMediator mediator,
    IRefreshTokenCarrier refreshTokenCarrier)
    : ControllerBase
{
[AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var orchestrator = new LoginOrchestrator(
            request.Email,
            request.Password);

        var result = await mediator.Send(
            orchestrator,
            cancellationToken);

        if (result.Success &&
            refreshTokenCarrier.RawRefreshToken is not null)
        {
            Response.Cookies.Append(
                "refreshToken",
                refreshTokenCarrier.RawRefreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax,
                    Expires =
                        DateTimeOffset.UtcNow.AddDays(7)
                });
        }

        var response = result.ToApiResponse();

        return StatusCode(
            response.StatusCode,
            response);
    }
}