

namespace Catalog.Features.CreateProduct;

public record CreateProductRequest(ProductDto Product);
public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : GenericEndpoint<CreateProductRequest, CreateProductResponse>
{

    public CreateProductEndpoint() : base("/products/create", "Create Product", RequestType.Command)
    {
        this.serviceNames = new List<string> { "status400" };
    }

}
