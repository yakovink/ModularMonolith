using System;
using Werhouse.Items.Models;

namespace Werhouse.Items.Features.GetItemById;

internal class GetItemByIdEndpoint : GenericGetEndpoint<Guid, HashSet<WerhouseItemHistory>>
{
    public GetItemByIdEndpoint() : base("/werhouse/get", "Get Werhouse Item History")
    {
        serviceNames = new List<string>{ "status404"};
    }

    protected override Task<IResult> NewEndpoint(Guid input, ISender sender)
    {
        return SendResults(new GetItemByIdQuery(input), sender);
    }
}
