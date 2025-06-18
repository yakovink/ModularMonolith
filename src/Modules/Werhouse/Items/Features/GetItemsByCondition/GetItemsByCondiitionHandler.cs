 

namespace Werhouse.Items.Features.GetItemsByCondition;


public class GetItemsByCondiitionHandler(IWerhouseRepository repository) : WerhouseModuleStructre.GetItemsByCondition
{
    public async Task<GenericResult<HashSet<WerhouseItemDto>>> Handle(WerhouseModuleStructre.GetItemsByCondition.Query request, CancellationToken cancellationToken)
    {
        if (request.input == null)
        {
            throw new ArgumentNullException(nameof(request.input), "Item condition cannot be null");
        }

        IEnumerable<WerhouseItem> items = await repository.GetItemsByCondition(true, item =>
            (request.input.id == null || item.Id == request.input.id) &&
            (request.input.ProductId == null || item.ProductId == request.input.ProductId) &&
            (request.input.InvoiceId == null || item.InvoiceId == request.input.InvoiceId) &&
            (request.input.Werhouse == null || item.Werhouse == request.input.Werhouse),
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
