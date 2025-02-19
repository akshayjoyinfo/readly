using ILogger = Serilog.ILogger;

namespace Readly.Api.Dependencies;

public static class MiddlewareConfig
{
    public static async Task<IApplicationBuilder> UseReadlyApiMiddleware(this WebApplication app, ILogger logger)
    {
        if (app.Environment.IsDevelopment())
        {
            //app.UseDeveloperExceptionPage();
            app.UseDefaultExceptionHandler(); // from FastEndpoints
            app.UseHsts();
            //app.UseShowAllServicesMiddleware(); // see https://github.com/ardalis/AspNetCoreStartupServices
        }
        else
        {
            app.UseDefaultExceptionHandler(); // from FastEndpoints
            app.UseHsts();
        }

        app.UseFastEndpoints()
            .UseSwaggerGen(); // Includes AddFileServer and static files middleware

        app.UseHttpsRedirection(); // Note this will drop Authorization headers
        logger.Information("===>>> Used ReadlyApiMiddleware ");
        await Task.CompletedTask;
        return app;
    }
}
