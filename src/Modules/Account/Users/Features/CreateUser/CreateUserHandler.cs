

namespace Account.Users.Features.CreateUser;

public class CreateUserHandler(IAccountRepository repository, ILogger<CreateUserHandler> logger)
    : AccountModuleStructre.CreateUser.IMEndpointPostHandler
{
    public async Task<GenericResult<Guid>> Handle(AccountModuleStructre.CreateUser.Command request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling CreateUserCommand for user with email: {Email}", request.input.Email);

        ArgumentException.ThrowIfNullOrEmpty(request.input.UserId, nameof(request.input.UserId));
        ArgumentException.ThrowIfNullOrEmpty(request.input.UserName, nameof(request.input.UserName));
        ArgumentException.ThrowIfNullOrEmpty(request.input.Email, nameof(request.input.Email));
        ArgumentException.ThrowIfNullOrEmpty(request.input.PhoneNumber, nameof(request.input.PhoneNumber));
        ArgumentException.ThrowIfNullOrEmpty(request.input.Address, nameof(request.input.Address));


        User user = await repository.CreateUser(request.input, cancellationToken);
        Console.WriteLine($"User created with ID: {user.Id}, UserName: {user.UserName}, Name: {user.FirstName} {user.LastName}, Email: {user.Email}, PhoneNumber: {user.PhoneNumber}, Address: {user.address}");

        return new GenericResult<Guid>(user.Id);
    }
}
