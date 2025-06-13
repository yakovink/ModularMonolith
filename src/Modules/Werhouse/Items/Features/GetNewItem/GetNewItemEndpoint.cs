using System;

namespace Werhouse.Items.Features.GetNewItem;

public class GetNewItemEndpoint : GenericPostEndpoint<Guid, Guid>
{
    public GetNewItemEndpoint() : base("/werhouse/getnew", "Get New Werhouse Item")
    {
        serviceNames = new List<string> { "status404" };
    }



    protected override async Task<IResult> NewEndpoint(GenericCommandRequest<Guid, Guid> request, ISender sender)
    {
        return await SendResults(new GetNewItemCommand(request.input), sender);
    }
}

