namespace exam_system.Features.Shared.Queries;


public sealed record GetStudentIdByUserIdQuery(Guid UserId): IRequest<Guid?>;