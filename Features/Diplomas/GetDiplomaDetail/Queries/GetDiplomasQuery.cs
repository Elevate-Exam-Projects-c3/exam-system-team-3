using exam_system.Features.Diplomas.GetDiplomas.DTOs;

namespace exam_system.Features.Diplomas.GetDiplomas.Queries;

public sealed record GetDiplomasQuery(
    int PageIndex = 1,
    int PageSize = 10)
    : IRequest<Result<PaginatedResult<DiplomaListItemResponse>>>;




