
namespace Catalog.Features.GetProductsByCondition;



public class GetProductsByConditionHandler(ICatalogRepository repository) : CatalogModuleStructre.GetProductsByCondition.IMEndpointGetHandler
{
    public async Task<GenericResult<HashSet<ProductDto>>> Handle(CatalogModuleStructre.GetProductsByCondition.Query request,
                  CancellationToken cancellationToken)
    {
        //get the product entity ID
        IEnumerable<Product> products = await repository.GetProductByCondition(request.input, true, cancellationToken);
        //map the products to DTOs
        HashSet<ProductDto> productsDto = products.Adapt<IEnumerable<ProductDto>>().ToHashSet();
        //return the result
        return new GenericResult<HashSet<ProductDto>>(productsDto);
    }

}
