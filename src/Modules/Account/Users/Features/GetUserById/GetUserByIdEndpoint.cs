 

namespace Account.Users.Features.GetUserById;



internal class GetUserByIdEndpoint : AccountModuleStructre.GetUserById.MGetEndpoint{
    public GetUserByIdEndpoint() : base("users/get", "Get User by ID")
    {
        this.serviceNames = new List<string>
        {
            "status404"
        };
    }

    protected async override Task<IResult> NewEndpoint(Guid input, ISender sender)
    {
        return await SendResults(new AccountModuleStructre.GetUserById.Query(input), sender);
    }


}
