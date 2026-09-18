using exam_system.Common;
using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Attempts.SubmitAttempt.Shared;
using exam_system.Persistence;
using exam_system.Persistence.Context;
using exam_system.Persistence.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddFeatureServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddCommonServices(builder.Configuration);

builder.Services.AddScoped<
    IAttemptFinalizationService,
    AttemptFinalizationService>();

var app = builder.Build();

app.UseExceptionHandler();
// Seed Database automatically on startup\\
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;                   
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        await AppDbContextSeed.SeedAsync(context, logger);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred during database migration/seeding.");
    }
}

// Enable Swagger UI in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Examination System API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
