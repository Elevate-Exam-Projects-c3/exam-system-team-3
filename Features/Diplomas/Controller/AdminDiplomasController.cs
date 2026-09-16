using exam_system.Features.Diplomas.AdminCreateDiploma.Commands;
using exam_system.Features.Diplomas.AdminCreateDiploma.DTOS;
using exam_system.Features.Diplomas.AdminDeleteDiploma.Orchestrator;
using exam_system.Features.Diplomas.AdminUpdateDiploma.Commands;
using exam_system.Features.Diplomas.AdminUpdateDiploma.DTOS;

namespace exam_system.Controllers;

[ApiController]
[Route("api/admin/diplomas")]
[Authorize(Roles = "Admin")]
public sealed class AdminDiplomasController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreateDiplomaResponse>>> CreateDiploma
                ([FromBody] CreateDiplomaCommand command, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(command, cancellationToken);
        var apiResponse = result.ToApiResponse();
        return StatusCode(apiResponse.StatusCode, apiResponse);
    }

    [HttpPut]
    public async Task<ActionResult<ApiResponse<UpdateDiplomaResponse>>> UpdateDiploma
                    ([FromBody] UpdateDiplomaCommand command, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(command, cancellationToken);
        var apiResponse = result.ToApiResponse();
        return StatusCode(apiResponse.StatusCode, apiResponse);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ApiResponse<Deleted>>> DeleteDiploma
                (Guid id, CancellationToken cancellationToken = default)
    {
        var command = new DeleteDiplomaOrchestratorL(id);
        var result = await sender.Send(command, cancellationToken);
        var apiResponse = result.ToApiResponse();
        return StatusCode(apiResponse.StatusCode, apiResponse);
    }
}