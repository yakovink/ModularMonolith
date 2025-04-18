using System;
using Catalog.Features.GetProductById;

namespace Catalog.Features.DeleteProduct;

public record DeleteProductCommand
    (Guid Id)
    : ICommand<DeleteProductResult>;


public record DeleteProductResult(bool IsDeleted);

internal class DeleteProductHandler(CatalogDbContext dbContext) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command,
                  CancellationToken cancellationToken)
    {
        //get the product entity ID
        Product product = await dbContext.getProductById(command.Id, cancellationToken);
        //delete the product
        DeleteProduct(product);
        //return the result
        await dbContext.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult(validate(command.Id));

    }

    private void DeleteProduct(Product product)
    {
        dbContext.Products.Remove(product);
    }

    private bool validate(Guid id)
    {
        return dbContext.Products.All(p => p.Id != id);
    }
}
