using System;

namespace Account.Users.Features.CreateUser;

public record CreateUserCommand(UserDto input) : ICommand<GenericResult<Guid>>;
public class CreateUserHandler(AccountDbContext dbContext, ILogger<CreateUserHandler> logger)
    : ICommandHandler<CreateUserCommand, GenericResult<Guid>>
{
    public async Task<GenericResult<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling CreateUserCommand for user with email: {Email}", request.input.Email);

        ArgumentException.ThrowIfNullOrEmpty(request.input.UserId, nameof(request.input.UserId));
        ArgumentException.ThrowIfNullOrEmpty(request.input.UserName, nameof(request.input.UserName));
        ArgumentException.ThrowIfNullOrEmpty(request.input.Email, nameof(request.input.Email));
        ArgumentException.ThrowIfNullOrEmpty(request.input.PhoneNumber, nameof(request.input.PhoneNumber));
        ArgumentException.ThrowIfNullOrEmpty(request.input.Address, nameof(request.input.Address));


        User user = await User.fromDto(request.input, cancellationToken);
        Console.WriteLine($"User created with ID: {user.Id}, UserName: {user.UserName}, Name: {user.FirstName} {user.LastName}, Email: {user.Email}, PhoneNumber: {user.PhoneNumber}, Address: {user.address}");
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new GenericResult<Guid>(user.Id);
    }
}
