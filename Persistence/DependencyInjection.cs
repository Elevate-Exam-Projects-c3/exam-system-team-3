using exam_system.Features.Shared.Results;
using exam_system.Persistence.Context;
using exam_system.Persistence.DataAccess;
using System.Reflection;
using exam_system.Features.Identity.Register.Orchestrators;
using exam_system.Features.Identity.VerifyEmailOtp.Orchestrators;
using exam_system.Features.Identity.VerifyEmailOtp.Validators;


namespace exam_system.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Server=(localdb)\\mssqllocaldb;Database=ExaminationSystemDb;Trusted_Connection=True;MultipleActiveResultSets=true";

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();


        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>)); 
        });
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
       
        services.AddScoped<RegisterUserOrchestrator>();
        services.AddScoped<VerifyEmailOtpRequestValidator>();
        services.AddScoped<VerifyEmailOtpOrchestrator>();

        return services;
    }
}
