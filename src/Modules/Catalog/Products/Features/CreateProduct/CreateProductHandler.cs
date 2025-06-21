
/*
mediatR is a messaging platform that use components memory
locations to transfer messages betwen components

*/
using Catalog.Data.Repositories;

namespace Catalog.Features.CreateProduct;


public class CreateProductCommandValidator : CatalogModuleStructre.CreateProduct.MValidator
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


internal class CreateProductHandler(ICatalogRepository repository,
ILogger<CreateProductHandler> logger) : CatalogModuleStructre.CreateProduct.IMEndpointPostHandler
{
    public async Task<GenericResult<Guid>> Handle(CatalogModuleStructre.CreateProduct.Command command,
                 CancellationToken cancellationToken)
    {

        logger.LogInformation("CreateProductCommandHandler.handle called with {@Command}", command);

        //create product entity
        Product product = await repository.CreateProduct(command.input,cancellationToken);

        return new GenericResult<Guid>(product.Id);
    }
}
