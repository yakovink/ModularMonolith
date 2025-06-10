using System;
using Werhouse.Items.Models;

namespace Werhouse.Data.Configurations;

public class WerhouseItemHistoryConfigurations : IEntityTypeConfiguration<WerhouseItemHistory>
{
    public void Configure(EntityTypeBuilder<WerhouseItemHistory> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property("In").IsRequired();
        builder.Property("_createdDate").IsRequired();
        builder.Property("operation").IsRequired();
        builder.Property("description").IsRequired();


    }
}
