using Microsoft.AspNetCore.Mvc;

#pragma warning disable CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.
namespace Readly.Api.Endpoints;

public static class SwaggerMetadataHelper
{
    public static Action<EndpointSummary<TRequest>> GetSwaggerMetadata<TRequest>(
        string summary, string description)
    {
        return s =>
        {
            s.Summary = summary;
            s.Description = description;
            s.Responses[200] = "Request Success";
            s.Responses[201] = "Resource Created";
            s.Responses[400] = "Invalid request data.";
            s.Responses[500] = "Internal server error."; // Common across all endpoints
        };
    }
}
