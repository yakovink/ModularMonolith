 

namespace Account.Users.Features.CreateUser;




internal class CreateUserEndpoint : AccountModuleStructre.CreateUser.MPostEndpoint
{
    public CreateUserEndpoint() : base("users/create", "Create User")
    {
        this.serviceNames = new List<string>
        {
            "status400",
            "status404"
        };
    }

    protected async override Task<IResult> NewEndpoint(GenericCommandRequest<UserDto, Guid> request, ISender sender)
    {
        return await SendResults(new AccountModuleStructre.CreateUser.Command(request.input), sender);
    }
}
