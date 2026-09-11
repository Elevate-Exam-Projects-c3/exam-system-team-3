using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Diplomas.EnrollDiploma.Quiers;

namespace exam_system.Features.Diplomas.EnrollDiploma.Handlers;

public sealed class DiplomaIsAvailableHandler(IGenericRepository<Diploma> diplomaRepository)
                                                 : IRequestHandler<DiplomaIsAvailableQuiery, bool>
{
    public async Task<bool> Handle(DiplomaIsAvailableQuiery request, CancellationToken cancellationToken)
    {
        var diplomaIsAvailable = await diplomaRepository
                                                      .Get(diploma =>
                                                      diploma.Id == request.DiplomaId && !diploma.IsDeleted &&
                                                      diploma.Quizzes.Any(quiz => !quiz.IsDeleted && quiz.Status == QuizStatus.Published))
                                                      .AnyAsync(cancellationToken);
        return diplomaIsAvailable;
    }
}