

using Account.Data.Repositories;

namespace Account.Users.Features.GetUserById;

public class GetUserByIdHandler(IAccountRepository repository, ILogger<GetUserByIdHandler> logger)
    : AccountModuleStructre.GetUserById.IMEndpointGetHandler
{
    public async Task<GenericResult<UserDto >> Handle(AccountModuleStructre.GetUserById.Query request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetUserByIdCommand for user with ID: {Id}", request.input);

        ArgumentException.ThrowIfNullOrEmpty(request.input.ToString(), nameof(request.input));
        User? user = await repository.GetUserById(request.input, true, cancellationToken);
        if (user == null)
        {
            throw new Exception($"User not found with ID {request.input}");
        }

        return new GenericResult<UserDto>(user.ToDto());
    }
}
