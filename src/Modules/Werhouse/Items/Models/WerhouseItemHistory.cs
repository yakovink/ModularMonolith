using System;
using System.Text.Json.Serialization;

namespace Werhouse.Items.Models;

public class WerhouseItemHistory : Entity<Guid>
{
    public int? In { get; private set; } = default!;
    public int? Out { get; private set; } = default!;
    public string? operation { get; private set; } = default!;
    public string? description { get; private set; } = default!;
    [JsonIgnore]
    public WerhouseItem item { get; private set; } = default!;
    public Guid WerhouseItemId { get; private set; } = default!;

    public static WerhouseItemHistory Create(int? inValue, int? outValue, string? operation, string? description, WerhouseItem item)
    {
        return new WerhouseItemHistory
        {
            Id = Guid.NewGuid(),
            In = inValue,
            Out = outValue,
            operation = operation,
            description = description,
            item = item,
            WerhouseItemId = item.Id
        };

    }
     
}
