namespace exam_system.Features.Diplomas.AdminCreateDiploma.DTOS;

public sealed record CreateDiplomaRequest(
    string Title,
    string? Description);