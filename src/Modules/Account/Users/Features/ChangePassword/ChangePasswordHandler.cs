 


namespace Account.Users.Features.ChangePassword;

public record ChangePasswordCommand(PasswordDto input) : ICommand<GenericResult<bool>>;


public class ChangePasswordHandler(AccountDbContext dbContext,ILogger<ChangePasswordHandler> logger) : ICommandHandler<ChangePasswordCommand, GenericResult<bool>>
{
    public async Task<GenericResult<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {


        logger.LogInformation("Handling ChangePasswordCommand for user with ID: {UserId}", request.input.Id);
        if (request.input.Id == null)
        {
            throw new InternalServerException("User ID cannot be null.");
        }
        User user = await dbContext.GetUserByIdAsync((Guid)request.input.Id,cancellationToken,RequestType.Command);
        user.UpdatePassword(request.input.NewPassword);
        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new GenericResult<bool>(true);
    }
}
