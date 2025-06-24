


namespace Catalog.Features.UpdateProduct;


public class UpdateProductCommandValidator : CatalogModuleStructre.UpdateProduct.MValidator
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.input)
            .NotNull()
            .WithMessage("Product cannot be null");
        RuleFor(x => x.input.Id)
            .NotEmpty()
            .WithMessage("Id cannot be empty");
        RuleFor(x => x.input)
            .Must(x => x.Name != null || x.Price != null || x.Description != null || x.ImageUrl != null || x.Categories != null)
            .WithMessage("At least one of Name, Price, Description or ImageUrl must be provided");

    }
}

internal class UpdateProductHandler(ICatalogRepository repository) : CatalogModuleStructre.UpdateProduct.IMEndpointPutHandler
{

    public async Task<GenericResult<bool>> Handle(CatalogModuleStructre.UpdateProduct.Command command,
                  CancellationToken cancellationToken)
    {
        bool success = await repository.UpdateProduct(command.input, cancellationToken);
        //return the result
        return new GenericResult<bool>(success);
    }
}
