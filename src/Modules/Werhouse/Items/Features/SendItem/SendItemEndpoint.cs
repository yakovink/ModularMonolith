

namespace Werhouse.Items.Features.SendItem;

internal class SendItemEndpoint : WerhouseModuleStructre.SendItem.MPutEndpoint
{
    public SendItemEndpoint() : base("/werhouse/send", "Send item")
    {
        serviceNames = new List<string>
        {
            "status404"
        };
    }

    protected async override Task<IResult> NewEndpoint(GenericCommandRequest<WerhouseItemDto, bool> request, ISender sender)
    {
        return await SendResults(new WerhouseModuleStructre.SendItem.Command(request.input), sender);
    }


}
