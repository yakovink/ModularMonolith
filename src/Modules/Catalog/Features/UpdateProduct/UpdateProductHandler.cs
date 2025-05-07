using System;

namespace Catalog.Features.UpdateProduct;
public record UpdateProductCommand
    (ProductDto product)
    : ICommand<UpdateProductResult>;


public record UpdateProductResult(bool isUpdated);

internal class UpdateProductHandler(CatalogDbContext dbContext) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{

    public async Task<UpdateProductResult> Handle(UpdateProductCommand command,
                  CancellationToken cancellationToken)
    {
        //get the product entity ID
        Product product = await dbContext.getProductById(command.product.Id, cancellationToken,RequestType.Command);
        //update the product
        UpdateProduct(product, command.product);
        //save to db
        await dbContext.SaveChangesAsync(cancellationToken);
        //validate
        bool success=await validate(product, cancellationToken);
        //return the result
        return new UpdateProductResult(success);
    }

    private void UpdateProduct(Product product, ProductDto productDto)
    {
        product.Update(
            productDto.Name,
            productDto.Categories,
            productDto.Price,
            productDto.Description,
            productDto.ImageUrl
        );
    }

    private async Task<bool> validate(Product product, CancellationToken cancellationToken)
    {
        Product UpdatedProduct=await dbContext.getProductById(product.Id, cancellationToken,RequestType.Command);
        return product.Compare(UpdatedProduct);
    }
}
