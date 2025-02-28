using Microsoft.Extensions.DependencyInjection;
using Readly.Application.Common.Interfaces;
using Readly.Infrastructure.Data;
using Readly.Infrastructure.Security;

namespace Readly.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureConfigs(this IServiceCollection services)
    {
        services.AddScoped<IReadlyDbContext>(provider =>
            provider.GetService<ReadlyDbContext>() ?? throw new InvalidOperationException());
        services.AddScoped<IJwtService, JwtService>();
        return services;
    }
}
