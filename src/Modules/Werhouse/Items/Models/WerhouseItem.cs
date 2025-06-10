using System;
using System.Text.Json.Serialization;

namespace Werhouse.Items.Models;

public class WerhouseItem : Aggregate<Guid>
{
    public Guid ProductId { get; private set; } = default!;
    public Guid InvoiceId { get; private set; } = default!;
    public int Werhouse { get; private set; } = default!;
    [JsonInclude]
    public List<WerhouseItemHistory> checkpoints { get; private set; } = new();

    

    
}
