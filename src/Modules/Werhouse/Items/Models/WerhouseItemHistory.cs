using System;
using System.Text.Json.Serialization;

namespace Werhouse.Items.Models;

public class WerhouseItemHistory : Entity<Guid>
{
    public string In { get; private set; } = default!;
    public string Out { get; private set; } = default!;
    public string operation { get; private set; } = default!;
    public string description { get; private set; } = default!;
    [JsonIgnore]
    public WerhouseItem item { get; private set; } = default!;
    public Guid WerhouseItemId { get; private set; } = default!;
     
}
