namespace exam_system.Features.Diplomas.AdminDeleteDiploma.Commands;


public sealed record DeleteDiplomaCommand(Guid Id): IRequest<Result<Deleted>>;