
/*
mediatR is a messaging platform that use components memory
locations to transfer messages betwen components

*/
namespace Catalog.Features.CreateProduct;

public record CreateProductCommand
    (ProductDto input)
    : ICommand<CreateProductResult>;


public record CreateProductResult(Guid Id):GenericResult<Guid>(Id);



public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.input)
            .NotNull()
            .WithMessage("Product cannot be null");
        RuleFor(x => x.input.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty");
        RuleFor(x => x.input.Price)
            .NotEmpty()
            .WithMessage("Price cannot be empty");
        RuleFor(x => x.input.Description)
            .NotEmpty()
            .WithMessage("Description cannot be empty");
        RuleFor(x => x.input.ImageUrl)
            .NotEmpty()
            .WithMessage("ImageUrl cannot be empty");
    }
}


internal class CreateProductHandler(CatalogDbContext dbContext,
ILogger<CreateProductHandler> logger) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command,
                 CancellationToken cancellationToken)
    {

        logger.LogInformation("CreateProductCommandHandler.handle called with {@Command}", command);

        //create product entity
        Product product = CreateNewProduct(command.input);
        //save to db
        dbContext.Products.Add(product);
        //return the result
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }

    private Product CreateNewProduct(ProductDto product)
    {
        if (product == null || product.Price == null || product.Name == null || product.Categories == null || product.Description == null || product.ImageUrl == null)
        {
            throw new ArgumentNullException(nameof(product));
        }


        List<ProductCategory> categories = product.Categories.Select(s =>
        {
            //parse the string to enum
            return (ProductCategory)Enum.Parse(typeof(ProductCategory), s);
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
