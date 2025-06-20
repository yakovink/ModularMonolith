 

namespace Werhouse.Items.Models;

public class WerhouseItem : Aggregate<Guid>
{
    public Guid ProductId { get; private set; } = default!;
    public Guid? InvoiceId { get; set; } = default!;
    public int? Werhouse { get; private set; } = default!;
    [JsonInclude]
    public List<WerhouseItemHistory> checkpoints { get; private set; } = new();


    public static WerhouseItem Create(Guid productId)
    {
        return new WerhouseItem
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            InvoiceId = null,
            Werhouse = null
        };
    }
    public void UpdateWerhouse()
    {
        Werhouse = checkpoints.Last().Out ?? null;
    }



}
