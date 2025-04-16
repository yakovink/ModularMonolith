
/*
mediatR is a messaging platform that use components memory
locations to transfer messages betwen components

*/
namespace Catalog.Features.CreateProduct;

public record CreateProductCommand
    (ProductDto Product)
    : ICommand<CreateProductResult>;


public record CreateProductResult(Guid Id);

internal class CreateProductHandler(CatalogDbContext dbContext) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command,
                 CancellationToken cancellationToken)
    {
        //create product entity

        Product product = CreateNewProduct(command.Product);
        //save to db
        dbContext.Products.Add(product);
        //return the result
        await dbContext.SaveChangesAsync(cancellationToken);
    
        return new CreateProductResult(product.Id);
    }

    private Product CreateNewProduct(ProductDto product)
    {
        return Product.Create(
            product.Id,
            product.Name,
            product.Categories,
            product.Price,
            product.Description,
            product.ImageUrl
        );
    }
}
