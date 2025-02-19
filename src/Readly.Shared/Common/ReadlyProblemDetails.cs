namespace Readly.Shared.Common;

public record ReadlyProblemDetails
{
    public required string Title { get; init; }
    public required int Status { get; init; }
    public required string Detail { get; init; }
    public string? TraceId { get; init; }
    public string? SpanId { get; init; }
}
