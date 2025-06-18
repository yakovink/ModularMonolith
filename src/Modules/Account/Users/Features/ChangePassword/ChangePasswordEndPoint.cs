 
namespace Account.Users.Features.ChangePassword;


internal class ChangePasswordEndPoint : AccountModuleStructre.ChangePassword.MPostEndpoint
{
    public ChangePasswordEndPoint() : base("users/pass", "Change Password")
    {
        this.serviceNames = new List<string> {
            "status400"
            };
    }

    protected async override Task<IResult> NewEndpoint(GenericCommandRequest<PasswordDto, bool> request, ISender sender)
    {
        return await SendResults(new AccountModuleStructre.ChangePassword.Command(request.input), sender);
    }
}
