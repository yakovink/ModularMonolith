

namespace Catalog.Features.DeleteProduct;


public class DeleteProductEndpoint:CatalogModuleStructre.DeleteProduct.MDeleteEndpoint
{
    public DeleteProductEndpoint() : base(
        "/products/delete",
         "Delete Product")
    {
        this.serviceNames= new List<string> {
             "status400",
              "status404"
              };
    }

    protected async override Task<IResult> NewEndpoint(GenericCommandRequest<Guid,bool> request, ISender sender)
    {
        return await SendResults(new CatalogModuleStructre.DeleteProduct.Command(request.input), sender);
    }
}
