using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Diplomas.AdminUpdateDiploma.Commands;
using exam_system.Features.Shared.Results.ErrorCodes;

namespace exam_system.Features.Diplomas.UpdateDiploma.Handlers;

public sealed class UpdateDiplomaCommandHandler(IGenericRepository<Diploma> diplomaRepository, IUnitOfWork unitOfWork)
                                               : IRequestHandler<UpdateDiplomaCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(UpdateDiplomaCommand request, CancellationToken cancellationToken)
    {
        var diploma = await diplomaRepository
            .Get(x => x.Id == request.Id && !x.IsDeleted)
            .FirstOrDefaultAsync(cancellationToken);

        if (diploma is null) return DiplomaErrors.NotFound;

        diploma.Title = request.Title.Trim();
        diploma.Description = string.IsNullOrWhiteSpace(request.Description) ? null : request.Description.Trim();
        diploma.UpdatedAt = DateTime.UtcNow;
        diplomaRepository.Update(diploma);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Updated;
    }
}