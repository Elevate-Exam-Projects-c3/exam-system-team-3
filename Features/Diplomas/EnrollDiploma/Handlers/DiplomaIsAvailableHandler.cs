using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Diplomas.EnrollDiploma.Queries;

namespace exam_system.Features.Diplomas.EnrollDiploma.Handlers;

public sealed class DiplomaIsAvailableHandler(IGenericRepository<Diploma> diplomaRepository)
                                                 : IRequestHandler<DiplomaIsAvailableQuery, bool>
{
    public async Task<bool> Handle(DiplomaIsAvailableQuery request, CancellationToken cancellationToken)
    {
        var diplomaIsAvailable = await diplomaRepository
                                                      .Get(diploma =>
                                                      diploma.Id == request.DiplomaId && !diploma.IsDeleted &&
                                                      diploma.Quizzes.Any(quiz => !quiz.IsDeleted && quiz.Status == QuizStatus.Published))
                                                      .AnyAsync(cancellationToken);
        return diplomaIsAvailable;
    }
}