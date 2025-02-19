using System.Reflection;
using ILogger = Serilog.ILogger;

namespace Readly.Api.Dependencies;

public static class MediatrConfigs
{
    public static IServiceCollection AddMediatrConfigs(this IServiceCollection services, ILogger logger)
    {
        var mediatRAssemblies = new[]
        {
            Assembly.GetAssembly(typeof(Program)) // Core
            // Assembly.GetAssembly(typeof(Program)) // UseCases
        };

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
        logger.Information("===>>> Adding Mediatr Configs");
        return services;
    }
}
