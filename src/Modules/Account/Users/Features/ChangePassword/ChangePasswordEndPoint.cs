using System;

namespace Account.Users.Features.ChangePassword;


internal class ChangePasswordEndPoint : GenericPutEndpoint<PasswordDto,bool>
{
    public ChangePasswordEndPoint() : base("users/pass", "Change Password")
    {
        this.serviceNames = new List<string> {
            "status400"
            };
    }

    protected async override Task<IResult> NewEndpoint(GenericCommand<PasswordDto, bool> request, ISender sender)
    {
        return await SendResults(new ChangePasswordCommand(request.input), sender);
    }
}
