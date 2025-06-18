 

namespace Werhouse.Data.Configurations;

public class WerhouseItemHistoryConfigurations : WerhouseModuleStructre.IWerhouseItemHistoryConfigurations.IMDataConfiguration
{
    public void Configure(EntityTypeBuilder<WerhouseItemHistory> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property("_createdDate").IsRequired();
        builder.Property("WerhouseItemId").IsRequired();


    }
}
