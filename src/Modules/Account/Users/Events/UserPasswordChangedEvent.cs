 ;

namespace Account.Users.Events;

public record UserPasswordChangedEvent(User user) : IDomainEvent;


