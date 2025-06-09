 ;

namespace Account.Users.Features.GetUsers;

public record GetUsersQuery(PaginationRequest input) : IQuery<GenericResult<PaginatedResult<UserDto> >>;
public class GetUsersHandler(AccountDbContext dbContext, ILogger<GetUsersHandler> logger) 
    : IQueryHandler<GetUsersQuery, GenericResult<PaginatedResult<UserDto> >>
{
    public async Task<GenericResult<PaginatedResult<UserDto> >> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetUsersQuery with pagination request: {Request}", request.input);

        ArgumentException.ThrowIfNullOrEmpty(request.input.ToString(), nameof(request.input));
        List<User> users = await dbContext.Users.AsNoTracking().ToListAsync(cancellationToken);
        var UsersDto = users.Select(user => user.ToDto());
        var TotalCount = await dbContext.Users.CountAsync(cancellationToken);
        
        PaginatedResult<UserDto> paginatedResult = new PaginatedResult<UserDto>(
            request.input.PageIndex,
            request.input.PageSize,
            TotalCount,
            UsersDto
        );
        
        return new GenericResult<PaginatedResult<UserDto> >(paginatedResult);
    }
}

