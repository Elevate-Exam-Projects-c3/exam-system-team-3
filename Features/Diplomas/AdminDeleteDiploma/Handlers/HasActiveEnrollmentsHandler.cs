using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Diplomas.AdminDeleteDiploma.Quiers;

namespace exam_system.Features.Diplomas.AdminDeleteDiploma.Handlers;

public class HasActiveEnrollmentsHandler(IGenericRepository<StudentEnrollment> enrollmentRepository)
                                        : IRequestHandler<HasActiveEnrollmentQuery, bool>
{
    public async Task<bool> Handle(HasActiveEnrollmentQuery request, CancellationToken cancellationToken)
    {
        return await enrollmentRepository
            .Get(x => x.DiplomaId == request.DiplomaId && !x.IsDeleted)
            .AnyAsync(cancellationToken);
    }
}
