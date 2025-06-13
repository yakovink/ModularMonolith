using System;
using Werhouse.Items.Dtos;

namespace Werhouse.Items.Features.PerformOperation;

internal class PerformOperationEndpoint : GenericPostEndpoint<WerhouseItemHistoryDto, Guid>
{

    public PerformOperationEndpoint() : base("/werhouse/operation", "Perform Operation on Werhouse Item")
    {
        serviceNames = new List<string> { "status404" };
    }

    protected override async Task<IResult> NewEndpoint(GenericCommandRequest<WerhouseItemHistoryDto, Guid> request, ISender sender)
    {
        return await SendResults(new PerformOperationCommand(request.input), sender);
    }
}
