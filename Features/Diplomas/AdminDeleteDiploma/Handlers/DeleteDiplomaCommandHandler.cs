using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Shared.Results.ErrorCodes;

namespace exam_system.Features.Diplomas.AdminDeleteDiploma.Handlers;

public sealed class DeleteDiplomaCommandHandler(IGenericRepository<Diploma> diplomaRepository, IUnitOfWork unitOfWork)
                                                : IRequestHandler<DeleteDiplomaCommand, Result<Deleted>>
{
    public async Task<Result<Deleted>> Handle(DeleteDiplomaCommand request, CancellationToken cancellationToken)
    {
        var diploma = await diplomaRepository
            .Get(diploma => diploma.Id == request.DiplomaId && !diploma.IsDeleted)
            .FirstOrDefaultAsync(cancellationToken);

        if (diploma is null)
            return DiplomaErrors.NotFound;

        diploma.IsDeleted = true;
        diploma.DeletedAt = DateTime.UtcNow;
        diploma.UpdatedAt = DateTime.UtcNow;

        diplomaRepository.Update(diploma);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Deleted;
    }
}