namespace Readly.Domain.Common;

public interface IHaveDomainEvent
{
    public List<DomainEvent> DomainEvents { get; set; }
}
