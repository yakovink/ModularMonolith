 

namespace Account.Users.Features.GetUserByCondition;

public record GetUserByConditionQuery(UserDto input) : IQuery<GenericResult<HashSet<UserDto>>>;

public class GetUserByConditionHandler(AccountDbContext dbContext, ILogger<GetUserByConditionHandler> logger)
    : IQueryHandler<GetUserByConditionQuery, GenericResult<HashSet<UserDto>>>
{
    public async Task<GenericResult<HashSet<UserDto>>> Handle(GetUserByConditionQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetUserByConditionCommand with condition: {Condition}", request.input);

        ArgumentException.ThrowIfNullOrEmpty(request.input.ToString(), nameof(request.input));
        var realusers = await filterUsers(request.input, cancellationToken);
        HashSet<UserDto> users = realusers.Select(user => user.ToDto()).ToHashSet();
        return new GenericResult<HashSet<UserDto>>(users);
    }

    public async Task<HashSet<User>> filterUsers(UserDto condition, CancellationToken cancellationToken)
    {
        var users = await dbContext.Users.AsNoTracking().ToHashSetAsync(cancellationToken);
        return users.Where(user =>
                (condition.UserName == null || user.UserName.Contains(condition.UserName)) &&
                (condition.Email == null || user.Email.Contains(condition.Email)) &&
                (condition.PhoneNumber == null || user.PhoneNumber.Contains(condition.PhoneNumber)) &&
                (condition.Address == null || user.address.ToString().Contains(condition.Address)))
            .ToHashSet();
    }
}
