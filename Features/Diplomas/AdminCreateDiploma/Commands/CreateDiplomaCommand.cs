namespace exam_system.Features.Diplomas.AdminCreateDiploma.Commands;

public sealed record CreateDiplomaCommand(string Title, string? Description) : IRequest<Result<Created>>;

