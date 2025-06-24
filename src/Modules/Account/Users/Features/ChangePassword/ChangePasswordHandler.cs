


namespace Account.Users.Features.ChangePassword;


public class ChangePasswordHandler(IAccountRepository repository,ILogger<ChangePasswordHandler> logger) : AccountModuleStructre.ChangePassword.IMEndpointPostHandler
{
    public async Task<GenericResult<bool>> Handle(AccountModuleStructre.ChangePassword.Command request, CancellationToken cancellationToken)
    {


        logger.LogInformation("Handling ChangePasswordCommand for user with ID: {UserId}", request.input.Id);
        if (request.input == null)
        {
            throw new InternalServerException("input cannot be null.");
        }
        bool result = await repository.ChangePassword(request.input, cancellationToken);

        return new GenericResult<bool>(result);
    }
}
