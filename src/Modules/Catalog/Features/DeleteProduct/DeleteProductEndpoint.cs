
namespace Catalog.Features.DeleteProduct;
public record DeleteProductRequest(Guid Id):GenericRequest<Guid>(Id);
public record DeleteProductResponse(bool isSuccess):GenericResponse<bool>(isSuccess);
public class DeleteProductEndpoint:GenericEndpoint<Guid,bool>
{
    public DeleteProductEndpoint() : base("/products/delete", "Delete Product", RequestType.Command)
    {
        this.serviceNames= new List<string> { "status400", "status404" };
    }

}
