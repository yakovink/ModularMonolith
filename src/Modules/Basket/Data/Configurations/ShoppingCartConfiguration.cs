 

namespace Basket.Data.Configurations;

public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasMany(c => c.items).WithOne(i => i.ShoppingCart).HasForeignKey(i => i.ShoppingCartId).OnDelete(DeleteBehavior.Cascade);
    }
}
