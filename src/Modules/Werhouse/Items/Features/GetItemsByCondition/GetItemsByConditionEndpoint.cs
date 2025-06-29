
namespace Werhouse.Items.Features.GetItemsByCondition;

internal class GetItemsByConditionEndpoint : WerhouseModuleStructre.GetItemsByCondition.MGetEndpoint
{
    public GetItemsByConditionEndpoint() : base("/werhouse/condition", "Get List Of Items By Condition")
    {
        serviceNames = new List<string> { "status404" };
    }


    protected async override Task<IResult> NewEndpoint(WerhouseItemDto input, ISender sender)
    {
        return await SendResults(new WerhouseModuleStructre.GetItemsByCondition.Query (input), sender);
    }
}
