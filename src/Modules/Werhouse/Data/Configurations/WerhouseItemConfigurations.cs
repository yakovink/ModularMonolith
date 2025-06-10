using System;
using Werhouse.Items.Models;

namespace Werhouse.Data.Configurations;

public class WerhouseItemConfigurations : IEntityTypeConfiguration<WerhouseItem>
{
    public void Configure(EntityTypeBuilder<WerhouseItem> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property("ProductId").IsRequired();
        builder.Property("Werhouse").IsRequired();
        //builder.HasMany(c => c.checkpoints).WithOne(i => i.item).HasForeignKey(i => i.WerhouseItemId).OnDelete(DeleteBehavior.Cascade);
    

    }
}
