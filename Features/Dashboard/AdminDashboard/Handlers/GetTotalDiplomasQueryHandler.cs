
using exam_system.Domain.Entities.Diplomas;

namespace exam_system.Features.Dashboard.AdminDashboard.Handlers;

public class GetTotalDiplomasQueryHandler(IGenericRepository<Diploma> diplomaRepository)
                                        : IRequestHandler<GetTotalDiplomasQuery, int>
{
    public async Task<int> Handle(GetTotalDiplomasQuery request, CancellationToken cancellationToken)
    => await diplomaRepository.GetAll().CountAsync(cancellationToken);
}
