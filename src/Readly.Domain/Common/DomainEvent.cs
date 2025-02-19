namespace Readly.Domain.Common;

public abstract class DomainEvent
{
    public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.Now;
}
