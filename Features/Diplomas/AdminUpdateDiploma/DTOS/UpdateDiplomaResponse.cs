namespace exam_system.Features.Diplomas.AdminUpdateDiploma.DTOS;

public sealed record UpdateDiplomaResponse(
    Guid Id,
    string Title,
    string? Description,
    DateTime? UpdatedAt);