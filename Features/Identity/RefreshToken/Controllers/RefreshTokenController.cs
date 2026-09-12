using exam_system.Common.Auth.RefreshToken;
using exam_system.Features.Identity.RefreshToken.DTOs.Response;
using exam_system.Features.Identity.RefreshToken.Orchestrators;
using exam_system.Features.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Identity.RefreshToken.Controllers;
[ApiController]
[Route("api/identity")]
public class RefreshTokenController(
    IMediator mediator,
    IRefreshTokenCarrier refreshTokenCarrier
    ) :ControllerBase
{
    [AllowAnonymous]
    [HttpPost("refresh")]

    public async Task<ActionResult<ApiResponse<RefreshTokenResponse>>> Refresh(CancellationToken cancellationToken)
    {
        var rawToken= Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(rawToken))
        {
            var unauthorized = ApiResponse<RefreshTokenResponse>.Fail(
                "No refresh token provided.", StatusCodes.Status401Unauthorized);
            return StatusCode(unauthorized.StatusCode, unauthorized);
            
        }
        
        var result = await mediator.Send(new RefreshTokenOrchestrator(rawToken),cancellationToken);
        
        if(result.Success&& refreshTokenCarrier.RawRefreshToken is not null)
            {
                Response.Cookies.Append("refreshToken",refreshTokenCarrier.RawRefreshToken,new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTime.Now.AddDays(7)
                    });
            }
        
        else if(refreshTokenCarrier.ShouldClearCookie)
            {
                Response.Cookies.Delete("refreshToken");
                
                }
        var response = result.ToApiResponse();
        
        return StatusCode(response.StatusCode, response);
        
    }
    
}