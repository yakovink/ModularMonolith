 ;

namespace Account.Users.EventHandlers;

public class UserLogoutEventHandler(ILogger<UserLogoutEventHandler> logger) : INotificationHandler<UserLogoutEvent>
{
    public Task Handle(UserLogoutEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Username: {UserName} logout at {time}", notification.user.UserName, notification.user.LastLogout);
        return Task.CompletedTask;
    }
}
