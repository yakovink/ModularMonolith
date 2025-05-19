
/*
mediatR is a messaging platform that use components memory
locations to transfer messages betwen components

*/
namespace Catalog.Features.CreateProduct;

public record CreateProductCommand
    (ProductDto Product)
    : ICommand<CreateProductResult>;


public record CreateProductResult(Guid Id):GenericResult<Guid>(Id);


internal class CreateProductHandler(CatalogDbContext dbContext) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command,
                 CancellationToken cancellationToken)
    {
        
        //create product entity
        Product product = CreateNewProduct(command.Product);
        //save to db
        dbContext.Products.Add(product);
        Console.WriteLine(product.ToString());
        //return the result
        await dbContext.SaveChangesAsync(cancellationToken);
    
        return new CreateProductResult(product.Id);
    }

    private Product CreateNewProduct(ProductDto product)
    {   
        if (product == null|| product.Price == null|| product.Name == null|| product.Categories == null|| product.Description == null|| product.ImageUrl == null)
        {
            throw new ArgumentNullException(nameof(product));
        }


        List<ProductCategory> categories = product.Categories.Select(s=>{
            //parse the string to enum
            return (ProductCategory)Enum.Parse(typeof(ProductCategory),s);
    }).ToList();

        return Product.Create(
            Guid.NewGuid(),
            product.Name,
            categories,
            (decimal)product.Price,
            product.Description,
            product.ImageUrl
            
        );
    }
}
