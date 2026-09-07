using exam_system.Features.Diplomas.GetDiplomas.DTOs;
using exam_system.Features.Diplomas.GetDiplomas.Queries;

namespace exam_system.Controllers;

[ApiController]
[Route("api/diplomas")]
[Authorize(Roles = "Student")]
public sealed class DiplomasController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<PaginatedResult<DiplomaListItemResponse>>>> GetDiplomas(
                                                        [FromQuery] int pageIndex = 1,
                                                        [FromQuery] int pageSize = 10,
                                                        CancellationToken cancellationToken = default)
    {

        var query = new GetDiplomasQuery(pageIndex, pageSize);
        var result = await sender.Send(query, cancellationToken);
        var apiResponse = result.ToRequestResponse().ToApiResponse();

        return StatusCode(apiResponse.StatusCode, apiResponse);
    }
}