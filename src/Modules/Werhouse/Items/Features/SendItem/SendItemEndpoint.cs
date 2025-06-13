using System;
using Werhouse.Items.Dtos;

namespace Werhouse.Items.Features.SendItem;

internal class SendItemEndpoint : GenericPutEndpoint<WerhouseItemDto, bool>
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
        return await SendResults(new SendItemCommand(request.input), sender);
    }
}
