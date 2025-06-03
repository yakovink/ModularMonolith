namespace Catalog.Features.DeleteProduct;

public record DeleteProductCommand
    (Guid input)
    : ICommand<GenericResult<bool>>;



public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.input)
            .NotEmpty()
            .WithMessage("Id cannot be empty");
    }
}

internal class DeleteProductHandler(CatalogDbContext dbContext) : ICommandHandler<DeleteProductCommand, GenericResult<bool>>
{
    public async Task<GenericResult<bool>> Handle(DeleteProductCommand command,
                  CancellationToken cancellationToken)
    {
        //get the product entity ID
        Product product = await dbContext.getProductById(command.input, cancellationToken, RequestType.Command);
        //delete the product
        DeleteProduct(product);
        //return the result
        await dbContext.SaveChangesAsync(cancellationToken);
        return new GenericResult<bool>(validate(command.input));

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
