using exam_system.Features.Diplomas.GetDiplomaDetail;
using exam_system.Features.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Controllers;

[ApiController]
[Route("api/diplomas")]
//[Authorize]
[AllowAnonymous] 
public sealed class DiplomasController(ISender sender): ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetDiplomas(
                                                  [FromQuery] int pageIndex = 1,
                                                  [FromQuery] int pageSize = 10,
                                                  CancellationToken cancellationToken = default)
    {
        var studentId = Guid.Parse("8f3c2a71-6d4e-4b92-a857-1c39e5d7f625");
        //var studentIdClaim = User.FindFirst("sub")?.Value ?? User.FindFirst("userId")?.Value;

        //if (!Guid.TryParse(studentIdClaim, out var studentId))
        //{
        //    var unauthorizedResponse = RequestResponse<object>.Fail("Unauthorized.", StatusCodes.Status401Unauthorized);
        //    var unauthorizedApiResponse = unauthorizedResponse.ToApiResponse();
        //    return StatusCode(unauthorizedApiResponse.StatusCode, unauthorizedApiResponse);
        //}

        var query = new GetDiplomasQuery(studentId,pageIndex,pageSize);
        var result = await sender.Send(query,cancellationToken);
        var apiResponse = result.ToRequestResponse().ToApiResponse();

        return StatusCode(apiResponse.StatusCode,apiResponse);
    }
}