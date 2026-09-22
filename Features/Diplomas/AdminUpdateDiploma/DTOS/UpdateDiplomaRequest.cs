namespace exam_system.Features.Diplomas.AdminUpdateDiploma.DTOS;

public sealed record UpdateDiplomaRequest(
    string Title,
    string? Description);