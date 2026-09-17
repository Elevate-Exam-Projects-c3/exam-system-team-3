using exam_system.Features.Diplomas.GetDiplomaDetail.DTOs;

namespace exam_system.Features.Diplomas.GetDiplomaDetail.Queries;


public sealed record GetDiplomaDetailsQuery(Guid DiplomaId) : IRequest<Result<DiplomaDetailsResponse>>;