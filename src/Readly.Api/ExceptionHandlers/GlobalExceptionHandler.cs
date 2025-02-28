using System.Diagnostics;
using FluentValidation;
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
        
        
        if (exception is ValidationException validationException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            var errors = validationException.Errors
                .Select(x => new { Field = x.PropertyName, Message = x.ErrorMessage })
                .ToList();

            await httpContext.Response.WriteAsJsonAsync(new
            {
                Message = "Validation failed",
                Errors = errors
            }, cancellationToken: cancellationToken);

            return true; // Exception was handled
        }


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
