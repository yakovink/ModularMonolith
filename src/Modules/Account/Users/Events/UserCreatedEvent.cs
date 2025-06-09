 

namespace Account.Users.Events;

public record UserCreatedEvent(User user) : IDomainEvent;
