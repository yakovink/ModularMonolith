 

namespace Werhouse.Items.Dtos;

public record WerhouseItemHistoryDto : WerhouseModuleStructre.IWerhouseItemHistoryConfigurations.MDto
{
    public Guid? Id { get; set; }
    public Guid? WerhouseItemId{ get; set; }
    public int? In { get; set; }
    public int? Out { get; set; }
    public string? Operation { get; set; }
    public string? description{ get; set; }

}
