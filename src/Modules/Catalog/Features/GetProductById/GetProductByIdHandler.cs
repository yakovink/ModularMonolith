using System;

namespace Catalog.Features.GetProductById;
public record GetProductByIdQuery
    (Guid id)
    : IQuery<GetProductByIdResult>;


public record GetProductByIdResult(Product Product);
internal class GetProductByIdHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{

    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query,
                  CancellationToken cancellationToken)
    {
        //get the product entity ID
        Product product= await dbContext.getProductById(query.id, cancellationToken);
        return new GetProductByIdResult(product);
    }
}
