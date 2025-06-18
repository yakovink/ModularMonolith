 

namespace Account.Users.Features.GetUserById;

public class GetUserByIdHandler(AccountDbContext dbContext, ILogger<GetUserByIdHandler> logger)
    : AccountModuleStructre.GetUserById.IMEndpointGetHandler
{
    public async Task<GenericResult<UserDto >> Handle(AccountModuleStructre.GetUserById.Query request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetUserByIdCommand for user with ID: {Id}", request.input);

        ArgumentException.ThrowIfNullOrEmpty(request.input.ToString(), nameof(request.input));
        User? user = await dbContext.GetUserByIdAsync(request.input, cancellationToken, RequestType.Query);
        
        if (user == null)
        {
            throw new Exception($"User not found with ID {request.input}");
        }

        return new GenericResult<UserDto >(user.ToDto());
    }
}
