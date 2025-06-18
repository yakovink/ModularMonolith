
namespace Catalog.Features.GetProducts;


internal class GetProductsHandler(CatalogDbContext dbContext) 
: CatalogModuleStructre.GetProducts.IMEndpointGetHandler
{
    public async Task<GenericResult<PaginatedResult<ProductDto>>> Handle(CatalogModuleStructre.GetProducts.Query request,
                  CancellationToken cancellationToken)
    {
        var pageIndex = request.input.PageIndex;
        var pageSize = request.input.PageSize;
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
        return new GenericResult<PaginatedResult<ProductDto>>(paginatedResult);
    }

    private List<ProductDto> mapProducts(List<Product> products)
    {
        return products.Adapt<List<ProductDto>>();
    }

}
