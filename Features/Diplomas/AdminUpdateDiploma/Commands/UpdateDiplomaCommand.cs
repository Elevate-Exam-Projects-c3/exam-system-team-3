using exam_system.Features.Diplomas.AdminUpdateDiploma.DTOS;

namespace exam_system.Features.Diplomas.AdminUpdateDiploma.Commands;

public sealed record UpdateDiplomaCommand(
    Guid Id,
    string Title,
    string? Description)
    : IRequest<Result<UpdateDiplomaResponse>>;