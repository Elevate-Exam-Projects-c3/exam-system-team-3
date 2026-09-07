using exam_system.Features.Diplomas.GetDiplomaDetail.DTOs;
using exam_system.Features.Shared;
using exam_system.Features.Shared.Results;

namespace exam_system.Features.Diplomas.GetDiplomaDetail;

public sealed record GetDiplomasQuery(Guid StudentId, int PageIndex = 1,int PageSize = 10): IRequest<Result<PaginatedResult<DiplomaListItemResponse>>>;