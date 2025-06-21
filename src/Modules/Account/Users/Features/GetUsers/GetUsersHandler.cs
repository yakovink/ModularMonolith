


using Account.Data.Repositories;

namespace Account.Users.Features.GetUsers;


public class GetUsersHandler(IAccountRepository repository, ILogger<GetUsersHandler> logger) :AccountModuleStructre.GetUsers.IMEndpointGetHandler
{
    public async Task<GenericResult<PaginatedResult<UserDto> >> Handle(AccountModuleStructre.GetUsers.Query request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetUsersQuery with pagination request: {Request}", request.input);

        ArgumentException.ThrowIfNullOrEmpty(request.input.ToString(), nameof(request.input));
        List<User> users = (await repository.GetUsers(true,cancellationToken)).ToList();
        var UsersDto = users.Select(user => user.ToDto());
        var TotalCount = users.Count();
        
        PaginatedResult<UserDto> paginatedResult = new PaginatedResult<UserDto>(
            request.input.PageIndex,
            request.input.PageSize,
            TotalCount,
            UsersDto
        );
        
        return new GenericResult<PaginatedResult<UserDto> >(paginatedResult);
    }


}

