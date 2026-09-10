using exam_system.Common.Email;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace exam_system.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SendGridOptions>(configuration.GetSection(SendGridOptions.SectionName));

        services.AddSingleton<SendGrid.ISendGridClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<SendGridOptions>>().Value;
            return new SendGrid.SendGridClient(options.ApiKey);
        });

        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}