using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Diplomas.AdminDeleteDiploma.Commands;
using exam_system.Features.Shared.Results.ErrorCodes;

namespace exam_system.Features.Diplomas.DeleteDiploma.Handlers;

public sealed class DeleteDiplomaCommandHandler(IGenericRepository<Diploma> diplomaRepository
                                                ,IGenericRepository<StudentEnrollment> enrollmentRepository,IUnitOfWork unitOfWork)
                                                : IRequestHandler<DeleteDiplomaCommand, Result<Deleted>>
{
    public async Task<Result<Deleted>> Handle(DeleteDiplomaCommand request,CancellationToken cancellationToken)
    {
        var diploma = await diplomaRepository
            .Get(x => x.Id == request.Id && !x.IsDeleted)
            .FirstOrDefaultAsync(cancellationToken);

        if (diploma is null)
            return DiplomaErrors.NotFound;

        var hasActiveEnrollments = await enrollmentRepository
            .Get(x =>x.DiplomaId == request.Id &&!x.IsDeleted)
            .AnyAsync(cancellationToken);

        if (hasActiveEnrollments)
            return DiplomaErrors.HasActiveEnrollments;

        diploma.IsDeleted = true;
        diploma.DeletedAt = DateTime.UtcNow;
        diploma.UpdatedAt = DateTime.UtcNow;

        diplomaRepository.Update(diploma);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<Deleted>.Success(Result.Deleted);
    }
}