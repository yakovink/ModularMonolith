 

namespace Basket.Data.Configurations;

public class ShoppingCartItemConfigurations : BasketModuleStructre.ICartItemConfigurations.IMDataConfiguration
{
    public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
    {

        builder.HasKey(x => x.Id);
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();

    }
}
