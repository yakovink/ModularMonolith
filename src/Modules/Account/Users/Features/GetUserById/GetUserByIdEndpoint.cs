using System;

namespace Account.Users.Features.GetUserById;



internal class GetUserByIdEndpoint : GenericGetEndpoint<Guid, UserDto>
{
    public GetUserByIdEndpoint() : base("users/get", "Get User by ID")
    {
        this.serviceNames = new List<string>
        {
            "status404"
        };
    }

    protected async override Task<IResult> NewEndpoint(Guid input, ISender sender)
    {
        return await SendResults(new GetUserByIdQuery(input), sender);
    }


}
