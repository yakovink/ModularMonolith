 ;


namespace Account.Users.EventHandlers;

public class UserPasswordChangedEventHandler(ILogger<UserPasswordChangedEventHandler> logger) : INotificationHandler<UserPasswordChangedEvent>
{
    public Task Handle(UserPasswordChangedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("User password changed: {UserId}, Username: {UserName}", notification.user.Id, notification.user.UserName);
        return Task.CompletedTask;
    }
}
