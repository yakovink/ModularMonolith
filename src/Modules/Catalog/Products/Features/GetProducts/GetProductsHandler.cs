
using Catalog.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Features.GetProducts;


internal class GetProductsHandler(ICatalogRepository repository) 
: CatalogModuleStructre.GetProducts.IMEndpointGetHandler
{
    public async Task<GenericResult<PaginatedResult<ProductDto>>> Handle(CatalogModuleStructre.GetProducts.Query request,
                  CancellationToken cancellationToken)
    {



        //get the product entity ID
        IEnumerable<Product> products = await repository.GetProducts(true, cancellationToken);

        var pageIndex = request.input.PageIndex;
        var pageSize = request.input.PageSize;
        var TotalCount = products.Count();

        List<ProductDto> productsDto = products.Adapt<IEnumerable<ProductDto>>().ToList();

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


}
