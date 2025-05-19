using System;

namespace Catalog.Features.UpdateProduct;
public record UpdateProductCommand
    (ProductDto product)
    : ICommand<UpdateProductResult>;


public record UpdateProductResult(bool isUpdated): GenericResult<bool>(isUpdated);

internal class UpdateProductHandler(CatalogDbContext dbContext) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{

    public async Task<UpdateProductResult> Handle(UpdateProductCommand command,
                  CancellationToken cancellationToken)
    {
        //get the product entity
        Guid id = command.product.Id;
        Product product = await dbContext.getProductById(id, cancellationToken,RequestType.Command);
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


        //update the product entity
        product.Update(
            productDto.Name==null?product.Name:productDto.Name,
            productDto.Categories == null ?product.Categories: productDto.Categories.Select(s=>(ProductCategory)Enum.Parse(typeof(ProductCategory),s)).ToList(),
            productDto.Price==null?product.Price:(decimal)productDto.Price,
            productDto.Description==null?product.Description:productDto.Description,
            productDto.ImageUrl==null?product.ImageFile:productDto.ImageUrl
        );
    }

    private async Task<bool> validate(Product product, CancellationToken cancellationToken)
    {
        Product UpdatedProduct=await dbContext.getProductById(product.Id, cancellationToken,RequestType.Command);
        return product.Compare(UpdatedProduct);
    }
}
