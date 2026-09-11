using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Diplomas.AdminDeleteDiploma.Quiers;

namespace exam_system.Features.Diplomas.AdminDeleteDiploma.Handlers;

public class HasActiveEnrollmentsHandler(IGenericRepository<StudentEnrollment> enrollmentRepository)
                                        : IRequestHandler<HasActiveEnrollmentsQuiery, bool>
{
    public async Task<bool> Handle(HasActiveEnrollmentsQuiery request, CancellationToken cancellationToken)
    {
        var hasActiveEnrollments = await enrollmentRepository
            .Get(x => x.DiplomaId == request.DiplomaId && !x.IsDeleted)
            .AnyAsync(cancellationToken);
        return hasActiveEnrollments;
    }
}
