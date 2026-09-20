using exam_system.Features.Diplomas.GetStudentDashboard.Orchestrators;

namespace exam_system.Features.Diplomas.GetStudentDashboard.Validators;

public class StudentDashboardOrchestratorValidator : AbstractValidator<StudentDashboardOrchestrator>
{
    public StudentDashboardOrchestratorValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}