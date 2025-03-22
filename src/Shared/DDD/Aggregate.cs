using System;

namespace Shared.DDD;

public abstract class Aggregate<Tid>: Entity<Tid>,IAggregate<Tid>
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    IReadOnlyCollection<IDomainEvent> IAggregate._domainEvents => throw new NotImplementedException();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }


    public IDomainEvent[] clearDomainEvents()
    {
        IDomainEvent[] events = _domainEvents.ToArray();
        _domainEvents.Clear();
        return events;
    }
}
