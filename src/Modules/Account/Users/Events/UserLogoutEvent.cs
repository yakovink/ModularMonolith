

namespace Account.Users.Events;

public record UserLogoutEvent(User user): IDomainEvent;

