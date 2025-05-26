
namespace Catalog.Features.GetProducts;

public record GetProductsQuery(PaginationRequest request)
: IQuery<GetProductsResult>;

public record GetProductsResult(PaginatedResult<ProductDto> Products):GenericResult<PaginatedResult<ProductDto>>(Products);
internal class GetProductsHandler(CatalogDbContext dbContext) 
: IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query,
                  CancellationToken cancellationToken)
    {
        var pageIndex = query.request.PageIndex;
        var pageSize = query.request.PageSize;
        var TotalCount = await dbContext.Products.CountAsync(cancellationToken);



        //get the product entity ID
        List<Product> products = await dbContext.Products
            .AsNoTracking()
            .OrderBy(P => P.Name)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        List<ProductDto> productsDto = mapProducts(products);

        //create a paginated result
        PaginatedResult<ProductDto> paginatedResult = new PaginatedResult<ProductDto>
        (
            pageIndex,
            pageSize,
            TotalCount,
            productsDto
        );
        //return the result
        return new GetProductsResult(paginatedResult);
    }

    private List<ProductDto> mapProducts(List<Product> products)
    {
        return products.Adapt<List<ProductDto>>();
    }

}
