using System;

namespace Shared.DDD;

public interface IAggregate: IEntity
{
    IReadOnlyCollection<IDomainEvent> _domainEvents { get; }
    IDomainEvent[] clearDomainEvents();
    void AddDomainEvent(IDomainEvent domainEvent);
}

public interface IAggregate<T> : IAggregate, IEntity<T>
{
}