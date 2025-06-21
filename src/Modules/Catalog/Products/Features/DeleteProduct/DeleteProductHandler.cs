using Catalog.Data.Repositories;

namespace Catalog.Features.DeleteProduct;


public class DeleteProductCommandValidator : CatalogModuleStructre.DeleteProduct.MValidator
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.input)
            .NotEmpty()
            .WithMessage("Id cannot be empty");
    }
}

internal class DeleteProductHandler(ICatalogRepository repository) : CatalogModuleStructre.DeleteProduct.IMEndpointDeleteHandler
{
    public async Task<GenericResult<bool>> Handle(CatalogModuleStructre.DeleteProduct.Command command,
                  CancellationToken cancellationToken)
    {
        //get the product entity ID
        bool result = await repository.DeleteProduct(command.input,cancellationToken);
        return new GenericResult<bool>(result);

    }


}
