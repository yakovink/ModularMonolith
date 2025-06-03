namespace Catalog.Features.GetProductsByCondition;

public record GetProductsByConditionQuery
    (ProductDto input)
    : IQuery<GenericResult<HashSet<ProductDto>>>;



public class GetProductsByConditionHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductsByConditionQuery, GenericResult<HashSet<ProductDto>>>
{
    public async Task<GenericResult<HashSet<ProductDto>>> Handle(GetProductsByConditionQuery Query,
                  CancellationToken cancellationToken)
    {
        //get the product entity ID
        HashSet<Product> products = await dbContext.Products.AsNoTracking().ToHashSetAsync(cancellationToken);
        //filter the products by the condition
        products = filterProducts(products, Query.input);
        //map the products to DTOs
        HashSet<ProductDto> productsDto = mapProducts(products);
        //return the result
        return new GenericResult<HashSet<ProductDto>>(productsDto);
    }

    private HashSet<ProductDto> mapProducts(HashSet<Product> products)
    {
        return products.Adapt<HashSet<ProductDto>>();
    }

    private HashSet<Product> filterProducts(HashSet<Product> products, ProductDto fake)
    {
        return products.Where(p => (fake.Name == null || p.Name.Contains(fake.Name)) &&
            (fake.Categories == null || fake.Categories.All(c => p.Categories.Contains((ProductCategory)Enum.Parse(typeof(ProductCategory), c)))) &&
            (p.Price == fake.Price || fake.Price == null) &&
            (fake.Description == null || p.Description.Contains(fake.Description))).ToHashSet();
    }
}
