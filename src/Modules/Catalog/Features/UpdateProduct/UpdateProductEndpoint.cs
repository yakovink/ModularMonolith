using System;
using Shared.GenericRootModule.Features;

namespace Catalog.Features.UpdateProduct;

public record UpdateProductRequest(ProductDto Product):GenericRequest<ProductDto>(Product);

public record UpdateProductResponse(bool isSuccess):GenericResponse<bool>(isSuccess);

internal class UpdateProductEndpoint: GenericEndpoint<ProductDto, bool>
{
    public UpdateProductEndpoint() : base("/products/update", "Update Product", RequestType.Command)
    {
        this.serviceNames= new List<string> { "status400", "status404" };
    }

}
