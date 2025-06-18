 


namespace Catalog.Features.GetProductById;


internal class GetProductByIdHandler(CatalogDbContext dbContext) : CatalogModuleStructre.GetProductById.IMEndpointGetHandler
{

    public async Task<GenericResult<ProductDto>> Handle(CatalogModuleStructre.GetProductById.Query query,
                  CancellationToken cancellationToken)
    {
        //get the product entity ID
        Product product= await dbContext.getProductById(query.input, cancellationToken,RequestType.Query);
        //check if product is null
        if (product == null)
        {
            throw new ProductNotFoundException(query.input);
        }

        ProductDto productDto = product.Adapt<ProductDto>();
        //return the result
        return new GenericResult<ProductDto>(productDto);
    }
}
