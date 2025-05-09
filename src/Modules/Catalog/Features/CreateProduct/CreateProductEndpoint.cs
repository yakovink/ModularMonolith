

namespace Catalog.Features.CreateProduct;

public record CreateProductRequest(ProductDto input):GenericRequest<ProductDto>(input);
public record CreateProductResponse(Guid Id):GenericResponse<Guid>(Id);

internal class CreateProductEndpoint : GenericEndpoint<ProductDto, Guid>
{

    public CreateProductEndpoint() : base("/products/create", "Create Product", RequestType.Command)
    {
        this.serviceNames = new List<string> { "status400" };
    }

}
