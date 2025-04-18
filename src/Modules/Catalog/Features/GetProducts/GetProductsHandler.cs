
namespace Catalog.Features.GetProducts;

public record GetProductsQuery()
: IQuery<GetProductsResult>;

public record GetProductsResult(HashSet<ProductDto> Products);
internal class GetProductsHandler(CatalogDbContext dbContext) 
: IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query,
                  CancellationToken cancellationToken)
    {
        //get the product entity ID
        HashSet<Product> products = await dbContext.Products.AsNoTracking().ToHashSetAsync(cancellationToken);
        HashSet<ProductDto> productsDto = mapProducts(products);

        //return the result
        return new GetProductsResult(productsDto);
    }

    private HashSet<ProductDto> mapProducts(HashSet<Product> products)
    {
        return products.Adapt<HashSet<ProductDto>>();
        //return products.Select(p => new ProductDto(p.Id,p.Name,p.Categories,p.Description,p.Price,p.ImageFile)).ToHashSet();
    }

}
