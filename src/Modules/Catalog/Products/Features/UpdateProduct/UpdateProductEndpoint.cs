 

namespace Catalog.Features.UpdateProduct;




internal class UpdateProductEndpoint: CatalogModuleStructre.UpdateProduct.MPutEndpoint
{
    public UpdateProductEndpoint() : base(
        "/products/update",
        "Update Product"
        )
    {
        this.serviceNames= new List<string> {
            "status400",
            "status404"
            };
    }

    protected async override Task<IResult> NewEndpoint(GenericCommandRequest<ProductDto,bool> request, ISender sender)
    {

        return await SendResults(new CatalogModuleStructre.UpdateProduct.Command (request.input), sender);  
    }
}
