using exam_system.Domain.Entities.Identity;
using exam_system.Features.Shared.Queries;

namespace exam_system.Features.Shared.Handlers;

public class GetStudentIdByUserIdQueryHandler(IGenericRepository<Student> studentRepository) : IRequestHandler<GetStudentIdByUserIdQuery, Guid?>
{
    public async Task<Guid?> Handle(GetStudentIdByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await studentRepository
               .Get(student =>
                   student.UserId == request.UserId &&
                   !student.IsDeleted)
               .AsNoTracking()
               .Select(student => (Guid?)student.Id)
               .FirstOrDefaultAsync(cancellationToken);
    }
}
