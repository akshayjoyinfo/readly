using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Readly.Shared.Common;

namespace Readly.Api.Middlewares;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
        var spanId = Activity.Current?.SpanId.ToString();

        logger.LogError(
            exception,
            "Exception occured Readly API. TraceId: {TraceId}, SpanId: {SpanId}",
            traceId,
            spanId
        );

        var customProblemDetails = new ReadlyProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Detail = "Exception occured Readly API while processing your request.",
            Title = "Internal Server Error",
            TraceId = traceId,
            SpanId = spanId ?? string.Empty
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        httpContext.Response.ContentType = "application/problem+json";
        await httpContext.Response.WriteAsJsonAsync(customProblemDetails, cancellationToken);

        return true;
    }
}
