using System;


namespace Catalog.Features.GetProductById;
public record GetProductByIdQuery
    (Guid input)
    : IQuery<GenericResult<ProductDto>>;


internal class GetProductByIdHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductByIdQuery, GenericResult<ProductDto>>
{

    public async Task<GenericResult<ProductDto>> Handle(GetProductByIdQuery query,
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
