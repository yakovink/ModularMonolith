 

namespace Account.Users.Features.GetUsers;


internal class GetUsersEndpoint : AccountModuleStructre.GetUsers.MGetEndpoint
{
    public GetUsersEndpoint() : base("users", "Get Users")
    {
        this.serviceNames = new List<string>
        {
            "status404"
        };
    }

    protected async override Task<IResult> NewEndpoint(PaginationRequest input, ISender sender)
    {
        return await SendResults(new AccountModuleStructre.GetUsers.Query(input), sender);
    }
}
