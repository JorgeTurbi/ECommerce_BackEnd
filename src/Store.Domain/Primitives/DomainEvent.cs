

namespace Store.Domain.Primitives;

public interface IDomainEvent
{
    Guid Id { get; }

}
public abstract record DomainEvent(Guid Id) : IDomainEvent;
