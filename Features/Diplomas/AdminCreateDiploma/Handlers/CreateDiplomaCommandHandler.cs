using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Diplomas.AdminCreateDiploma.Commands;

public sealed class CreateDiplomaCommandHandler(IGenericRepository<Diploma> diplomaRepository, IUnitOfWork unitOfWork)
                                                : IRequestHandler<CreateDiplomaCommand, Result<Created>>
{
    public async Task<Result<Created>> Handle(
        CreateDiplomaCommand request,
        CancellationToken cancellationToken)
    {
        var diploma = new Diploma
        {
            Title = request.Title.Trim(),
            Description = string.IsNullOrWhiteSpace(request.Description) ? null : request.Description.Trim()
        };

        await diplomaRepository.AddAsync(diploma);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Created;
    }
}