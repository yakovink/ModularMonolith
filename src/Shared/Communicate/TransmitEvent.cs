
namespace Shared.Communicate
{
    // Abstract base for MassTransit in-memory events
    public abstract class TransmitEvent<TEvent>
    {
        public Guid EventId { get; } = Guid.NewGuid();
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
        public TEvent Payload { get; }

        protected TransmitEvent(TEvent payload)
        {
            Payload = payload;
        }

        // Derived classes must implement how to publish the event
        public abstract Task<bool> PublishAsync();
    }

    // Abstract handler for in-memory TransmitEvent events
    public abstract class TransmitEventHandler<TEvent> where TEvent : TransmitEvent<object>
    {
        // Handle (catch and process) the event
        public abstract Task HandleAsync(TEvent transmitEvent);
    }
}
