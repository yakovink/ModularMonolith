 ;


namespace Account.Users.EventHandlers;

public class UserCreatedDomainHandler(ILogger<UserCreatedDomainHandler> logger) : INotificationHandler<UserCreatedEvent>
{
    public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("User created: {UserId}, Username: {UserName}", notification.user.Id,notification.user.UserName);
        return Task.CompletedTask;
    }
}
