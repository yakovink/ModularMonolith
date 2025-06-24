



namespace Catalog.Features.GetProductById;


internal class GetProductByIdHandler(ICatalogRepository repository) : CatalogModuleStructre.GetProductById.IMEndpointGetHandler
{

    public async Task<GenericResult<ProductDto>> Handle(CatalogModuleStructre.GetProductById.Query query,
                  CancellationToken cancellationToken)
    {
        //get the product entity ID
        Product product = await repository.GetProductById(query.input,true, cancellationToken);

        ProductDto productDto = product.Adapt<ProductDto>();
        //return the result
        return new GenericResult<ProductDto>(productDto);
    }
}
