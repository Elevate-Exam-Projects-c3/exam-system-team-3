using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Diplomas.AdminCreateDiploma.Commands;
using exam_system.Features.Diplomas.AdminCreateDiploma.DTOS;

namespace exam_system.Features.Diplomas.AdminCreateDiploma.Handlers;

public class CreateDiplomaCommandHandler(
    IGenericRepository<Diploma> diplomaRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateDiplomaCommand, Result<CreateDiplomaResponse>>
{
    public async Task<Result<CreateDiplomaResponse>> Handle(CreateDiplomaCommand request, CancellationToken cancellationToken)
    {
        var diploma = new Diploma
        {
            Title = request.Title.Trim(),
            Description = string.IsNullOrWhiteSpace(request.Description)? null: request.Description.Trim()
        };
        await diplomaRepository.AddAsync(diploma);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CreateDiplomaResponse>.Success(new CreateDiplomaResponse(
            Id: diploma.Id,
            Title: diploma.Title,
            Description: diploma.Description
        ));
    }
}
