using exam_system.Common.Auth.Jwt;
using exam_system.Common.Auth.RefreshToken;
using exam_system.Common.Email;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using exam_system.Features.Shared;

namespace exam_system.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddCommonServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<SendGridOptions>(
            configuration.GetSection(SendGridOptions.SectionName));

        services.AddSingleton<SendGrid.ISendGridClient>(sp =>
        {
            var options = sp
                .GetRequiredService<IOptions<SendGridOptions>>()
                .Value;

            return new SendGrid.SendGridClient(options.ApiKey);
        });

        services.AddScoped<IEmailService, EmailService>();

        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.SectionName));

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtOptions = configuration
                    .GetSection(JwtOptions.SectionName)
                    .Get<JwtOptions>()!;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Convert.FromBase64String(jwtOptions.SecretKey)),

                    RoleClaimType = ClaimTypes.Role
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();

                        var response = ApiResponse<object>.Fail(
                            "Authentication is required.",
                            StatusCodes.Status401Unauthorized);

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                        await context.Response.WriteAsJsonAsync(response);
                    },

                    OnForbidden = async context =>
                    {
                        var response = ApiResponse<object>.Fail(
                            "You do not have permission to access this resource.",
                            StatusCodes.Status403Forbidden);

                        context.Response.StatusCode = StatusCodes.Status403Forbidden;

                        await context.Response.WriteAsJsonAsync(response);
                    }
                };
            });

        services.AddAuthorization();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IRefreshTokenCarrier, RefreshTokenCarrier>();

        return services;
    }
}