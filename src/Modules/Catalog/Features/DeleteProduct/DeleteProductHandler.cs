namespace Catalog.Features.DeleteProduct;

public record DeleteProductCommand
    (Guid input)
    : ICommand<DeleteProductResult>;


public record DeleteProductResult(bool IsDeleted): GenericResult<bool>(IsDeleted);

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.input)
            .NotEmpty()
            .WithMessage("Id cannot be empty");
    }
}

internal class DeleteProductHandler(CatalogDbContext dbContext) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command,
                  CancellationToken cancellationToken)
    {
        //get the product entity ID
        Product product = await dbContext.getProductById(command.input, cancellationToken, RequestType.Command);
        //delete the product
        DeleteProduct(product);
        //return the result
        await dbContext.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult(validate(command.input));

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
