 

namespace Account.Users.Events;

public record UserLoginEvent(User user): IDomainEvent;

