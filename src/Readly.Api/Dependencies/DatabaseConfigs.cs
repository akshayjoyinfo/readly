using Microsoft.EntityFrameworkCore;
using Readly.Infrastructure.Data;
using ILogger = Serilog.ILogger;

namespace Readly.Api.Dependencies;

public static class DatabaseConfigs
{
    public static IServiceCollection AddDatabaseConfigs(this IServiceCollection services, ILogger logger,
        IConfiguration configuration)
    {
        services.AddDbContext<ReadlyDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("ReadlyPGConnectionString"))
                .UseSnakeCaseNamingConvention();
            // options.EnableSensitiveDataLogging(); -- enable when u really want to see the Ef query generate logs
        });
        return services;
    }
}
