using System;
using Shared.GenericRootModule.Features;

namespace Catalog.Features.UpdateProduct;

public record UpdateProductRequest(ProductDto Product);
public record UpdateProductResponse(bool isSuccess);

internal class UpdateProductEndpoint: GenericEndpoint<UpdateProductRequest, UpdateProductResponse>
{
    public UpdateProductEndpoint() : base("/products/update", "Update Product", RequestType.Command)
    {
        this.serviceNames= new List<string> { "status400", "status404" };
    }

}
