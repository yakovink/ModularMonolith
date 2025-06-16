using System;

namespace Werhouse.Items.Dtos;

public record WerhouseItemDto : WerhouseModuleStructre.IMModelConfiguration
{
    public Guid? id { get; set; }
    public Guid? ProductId { get; set; }
    public Guid? InvoiceId { get; set; }
    public int? Werhouse { get; set; }
}
