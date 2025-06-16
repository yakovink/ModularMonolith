using System;
using Werhouse.Data;
using Werhouse.Data.Repositories;
using Werhouse.Items.Models;

namespace Werhouse.Items.Features.GetItemById;

using Shared.CQRS;
using Shared.GenericRootModule.Features;
using MediatR;
using Werhouse.Items.Dtos;
using Shared.Mechanism;

public record GetItemByIdQuery(Guid input) 
    : WerhouseModuleStructre.GetItemById.MEndpointGetRequest;

public class GetItemByIdHandler(IWerhouseRepository repository) : WerhouseModuleStructre.GetItemById.IMEndpointGetHandler<GetItemByIdQuery>
{
    public async Task<GenericResult<HashSet<WerhouseItemHistoryDto>>> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<WerhouseItemHistoryDto> itemHistory = (await repository.GetItemHistory(request.input, true, cancellationToken)).Select(i=>i.ToDto());
        Console.WriteLine($"Item history count for item {request.input}: {itemHistory.Count()}");
        
        return new GenericResult<HashSet<WerhouseItemHistoryDto>>(itemHistory.ToHashSet());

    }


}
