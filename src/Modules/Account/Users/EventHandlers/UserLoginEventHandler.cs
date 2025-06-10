 

namespace Account.Users.EventHandlers;

public class UserLoginEventHandler(ILogger<UserLoginEventHandler> logger) : INotificationHandler<UserLoginEvent>
{
    public Task Handle(UserLoginEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Username: {UserName} login at {time}", notification.user.UserName, notification.user.LastLogin);
        return Task.CompletedTask;
    }

}
