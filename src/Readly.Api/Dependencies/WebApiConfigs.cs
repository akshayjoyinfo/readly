using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Readly.Api.Middlewares;
using ILogger = Serilog.ILogger;

namespace Readly.Api.Dependencies;

public static class WebApiConfigs
{
    public static IServiceCollection AddWebApiConfigs(this IServiceCollection services, ILogger logger,
        IConfiguration configuration)
    {
        services.AddProblemDetails();
        services.AddSingleton<IExceptionHandler, GlobalExceptionHandler>();
        services.AddCors(option =>
        {
            option.AddPolicy(
                "ReadlyCorsPolicy",
                builder =>
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin());
        });
        services.AddFastEndpoints().SwaggerDocument(o =>
        {
            o.ShortSchemaNames = true;
        });
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    // Set the valid issuer and audience
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"] ??
                                               throw new InvalidOperationException("Not configured Jwt:SecretKey"))
                    )
                };
            });

        logger.Information("===>>> Adding WebApi Configs");
        return services;
    }
}
