using exam_system.Features.Dashboard.AdminDashboard.DTOs;

namespace exam_system.Features.Dashboard.AdminDashboard.Orchestrators;

public sealed record AdminDashboardOrchestrator : IRequest<Result<AdminDashboardResponse>>;
