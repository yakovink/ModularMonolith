using System;
using Werhouse.Data.Repositories;
using Werhouse.Items.Dtos;
using Werhouse.Items.Models;

namespace Werhouse.Items.Features.GetItemsByCondition;
public record GetItemsByConditionQuery(WerhouseItemDto item) : IQuery<GenericResult<HashSet<WerhouseItemDto>>>;


public class GetItemsByCondiitionHandler(IWerhouseRepository repository)
    : IQueryHandler<GetItemsByConditionQuery, GenericResult<HashSet<WerhouseItemDto>>>
{
    public async Task<GenericResult<HashSet<WerhouseItemDto>>> Handle(GetItemsByConditionQuery request, CancellationToken cancellationToken)
    {
        if (request.item == null)
        {
            throw new ArgumentNullException(nameof(request.item), "Item condition cannot be null");
        }

        IEnumerable<WerhouseItem> items = await repository.GetItemsByCondition(true, item =>
            (request.item.id == null || item.Id == request.item.id) &&
            (request.item.ProductId == null || item.ProductId == request.item.ProductId) &&
            (request.item.InvoiceId == null || item.InvoiceId == request.item.InvoiceId) &&
            (request.item.Werhouse == null || item.Werhouse == request.item.Werhouse),
            cancellationToken);
        var result = items.Select(item => new WerhouseItemDto
        {
            id = item.Id,
            ProductId = item.ProductId,
            InvoiceId = item.InvoiceId,
            Werhouse = item.Werhouse
        }).ToHashSet();

        return new GenericResult<HashSet<WerhouseItemDto>>(result);
    }
}
