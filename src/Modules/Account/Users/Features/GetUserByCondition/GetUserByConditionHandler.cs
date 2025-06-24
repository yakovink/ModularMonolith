


namespace Account.Users.Features.GetUserByCondition;


public class GetUserByConditionHandler(IAccountRepository repository, ILogger<GetUserByConditionHandler> logger)
    : AccountModuleStructre.GetUsersByCondition.IMEndpointGetHandler
{
    public async Task<GenericResult<HashSet<UserDto>>> Handle(AccountModuleStructre.GetUsersByCondition.Query request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetUserByConditionCommand with condition: {Condition}", request.input);

        ArgumentException.ThrowIfNullOrEmpty(request.input.ToString(), nameof(request.input));
        var realusers = await filterUsers(request.input, cancellationToken);
        HashSet<UserDto> users = realusers.Select(user => user.ToDto()).ToHashSet();
        return new GenericResult<HashSet<UserDto>>(users);
    }

    public async Task<IEnumerable<User>> filterUsers(UserDto condition, CancellationToken cancellationToken)
    {
        Expression<Func<User, bool>> filter = user =>
         (condition.UserName == null || user.UserName.Contains(condition.UserName)) &&
        (condition.Email == null || user.Email.Contains(condition.Email)) &&
        (condition.PhoneNumber == null || user.PhoneNumber.Contains(condition.PhoneNumber)) &&
        (condition.Address == null || user.address.ToString().Contains(condition.Address));


        return await repository.GetUserByCondition(filter, true, cancellationToken);

    }
}
