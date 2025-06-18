 
namespace Account.Users.Features.GetUserByCondition;


internal class GetUserByConditionEndpoint : AccountModuleStructre.GetUsersByCondition.MGetEndpoint
{

    public GetUserByConditionEndpoint() : base("users/condition", "Get users by condition")
    {
        this.serviceNames = new List<string>
        {
            "status400",
            "status404"
        };
    }
    protected async override Task<IResult> NewEndpoint(UserDto input, ISender sender)
    {
        return await SendResults(new AccountModuleStructre.GetUsersByCondition.Query(input), sender);
    }
}
