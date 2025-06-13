using System;
using Werhouse.Data;
using Werhouse.Data.Repositories;
using Werhouse.Items.Models;

namespace Werhouse.Items.Features.GetItemById;

using Shared.CQRS;
using Shared.GenericRootModule.Features;
using MediatR;

public record GetItemByIdQuery(Guid input) 
    : IQuery<GenericResult<HashSet<WerhouseItemHistory>>>;

public class GetItemByIdHandler(IWerhouseRepository repository) : IQueryHandler<GetItemByIdQuery, GenericResult<HashSet<WerhouseItemHistory>>>
{
    public async Task<GenericResult<HashSet<WerhouseItemHistory>>> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<WerhouseItemHistory> itemHistory = await repository.GetItemHistory(request.input, true, cancellationToken) ?? throw new NotFoundException($"Item with id {request.input} not found");
        return new GenericResult<HashSet<WerhouseItemHistory>>(itemHistory.ToHashSet());

    }
}
