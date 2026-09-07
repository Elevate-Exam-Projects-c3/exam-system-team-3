using exam_system.Features.Diplomas.GetDiplomaDetail;
using exam_system.Features.Diplomas.GetDiplomaDetail.DTOs;
using exam_system.Features.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace exam_system.Controllers;

[ApiController]
[Route("api/diplomas")]
[Authorize(Roles = "Student")]
public sealed class DiplomasController(ISender sender): ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<PaginatedResult<DiplomaListItemResponse>>>> GetDiplomas(
                                                  [FromQuery] int pageIndex = 1,
                                                  [FromQuery] int pageSize = 10,
                                                  CancellationToken cancellationToken = default)
    {
        var studentIdClaim =
                    User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                    User.FindFirst("sub")?.Value ??
                    User.FindFirst("userId")?.Value;
        if (!Guid.TryParse(studentIdClaim, out var studentId))
        {
            var response = RequestResponse<PaginatedResult<DiplomaListItemResponse>>
                .Fail( "Unauthorized.",StatusCodes.Status401Unauthorized).ToApiResponse();

            return StatusCode(response.StatusCode, response);
        }


        var query = new GetDiplomasQuery(studentId,pageIndex,pageSize);
        var result = await sender.Send(query,cancellationToken);
        var apiResponse = result.ToRequestResponse().ToApiResponse();

        return StatusCode(apiResponse.StatusCode,apiResponse);
    }
}