

namespace Account.Data.Configurations;

public class UserConfigurauions : AccountModuleStructre.IAccountConfigurations.IMDataConfiguration
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.UserId).IsUnique();
        builder.Property(e => e.UserId).IsRequired().IsFixedLength().HasMaxLength(9);

        builder.HasIndex(e => e.UserName).IsUnique();
        builder.Property(e => e.UserName).IsRequired().HasMaxLength(100);

        builder.HasIndex(e => e.Email).IsUnique();
        builder.Property(e => e.Email).IsRequired().HasMaxLength(100);

        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(20);

        builder.Property(e => e.LastName).IsRequired().HasMaxLength(20);

        builder.HasIndex(e => e.PhoneNumber).IsUnique();
        builder.Property(e => e.PhoneNumber).HasMaxLength(12);
        
        builder.Property(e => e.address).HasMaxLength(200);
    }


}
