 

namespace Shared.DDD;

public interface IDomainEvent:MediatR.INotification
{
    Guid EventId => new Guid();
    DateTime OccurredOn => DateTime.Now;
    public string EventType=>GetType().AssemblyQualifiedName!;

}
