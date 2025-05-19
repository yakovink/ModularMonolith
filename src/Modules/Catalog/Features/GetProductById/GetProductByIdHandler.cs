using System;

namespace Catalog.Features.GetProductById;
public record GetProductByIdQuery
    (Guid id)
    : IQuery<GetProductByIdResult>;


public record GetProductByIdResult(ProductDto Product): GenericResult<ProductDto>(Product);
internal class GetProductByIdHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{

    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query,
                  CancellationToken cancellationToken)
    {
        //get the product entity ID
        Product product= await dbContext.getProductById(query.id, cancellationToken,RequestType.Query);
        ProductDto productDto = product.Adapt<ProductDto>();
        //return the result
        return new GetProductByIdResult(productDto);
    }
}
