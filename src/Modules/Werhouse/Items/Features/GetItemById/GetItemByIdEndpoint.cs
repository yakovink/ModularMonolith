
namespace Werhouse.Items.Features.GetItemById;

internal class GetItemByIdEndpoint : WerhouseModuleStructre.GetItemById.MGetEndpoint
{
    public GetItemByIdEndpoint() : base("/werhouse/get", "Get Werhouse Item History")
    {
        serviceNames = new List<string>{ "status404"};
    }

    protected override Task<IResult> NewEndpoint(Guid input, ISender sender)
    {
        return SendResults(new WerhouseModuleStructre.GetItemById.Query(input), sender);
    }
}
