using Readly.Application;
using Readly.Infrastructure;
using ILogger = Serilog.ILogger;

namespace Readly.Api.Dependencies;

public static class DependencyInjection
{
    public static void AddApiServices(this WebApplicationBuilder builder, ILogger logger, IConfiguration configuration)
    {
        // Serilog
        builder.Host.UseSerilog((context, config) =>
        {
            config.ReadFrom.Configuration(context.Configuration)
                .Enrich.FromLogContext();
        });


        builder.Services
            .AddWebApiConfigs(logger, configuration)
            .AddApplicationServices()
            .AddInfrastructureConfigs();
    }
}
