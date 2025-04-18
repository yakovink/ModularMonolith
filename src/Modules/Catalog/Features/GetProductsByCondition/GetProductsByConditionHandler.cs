using System;

namespace Catalog.Features.GetProductsByCondition;

public record GetProductsByConditionQuery
    (ProductDto product)
    : IQuery<GetProductsByConditionResult>;


public record GetProductsByConditionResult(HashSet<ProductDto> Products);
public class GetProductsByConditionHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductsByConditionQuery, GetProductsByConditionResult>
{
    public async Task<GetProductsByConditionResult> Handle(GetProductsByConditionQuery Query,
                  CancellationToken cancellationToken)
    {
        //get the product entity ID
        HashSet<Product> products = await dbContext.Products.ToHashSetAsync(cancellationToken);
        //filter the products by the condition
        products = filterProducts(products, Query.product);
        //map the products to DTOs
        HashSet<ProductDto> productsDto = mapProducts(products);
        //return the result
        return new GetProductsByConditionResult(productsDto);
    }

    private HashSet<ProductDto> mapProducts(HashSet<Product> products)
    {
        return products.Adapt<HashSet<ProductDto>>();
    }

    private HashSet<Product> filterProducts(HashSet<Product> products, ProductDto fake)
    {
        return products.Where(p => (p.Name.Contains(fake.Name) || fake.Name==null) &&
            (fake.Categories.All(c=>p.Categories.Contains(c))|| fake.Categories==null) &&
            (p.Price == fake.Price || fake.Price == 0) &&
            (p.Description.Contains(fake.Description) || fake.Description==null) ).ToHashSet();
    }   
}
