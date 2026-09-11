namespace exam_system.Features.Diplomas.AdminCreateDiploma.DTOS;

public sealed record CreateDiplomaResponse(
    Guid Id,
    string Title,
    string? Description);