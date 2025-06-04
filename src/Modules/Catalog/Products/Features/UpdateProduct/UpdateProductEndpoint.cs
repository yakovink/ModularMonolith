using System;
using Shared.GenericRootModule.Features;

namespace Catalog.Features.UpdateProduct;




internal class UpdateProductEndpoint: GenericPutEndpoint<ProductDto, bool>
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

        return await SendResults(new UpdateProductCommand(request.input), sender);  
    }
}
