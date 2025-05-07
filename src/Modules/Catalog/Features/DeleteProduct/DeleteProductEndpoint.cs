
namespace Catalog.Features.DeleteProduct;
public record DeleteProductRequest(Guid Id);
public record DeleteProductResponse(bool isSuccess);
public class DeleteProductEndpoint:GenericEndpoint<DeleteProductRequest,DeleteProductResponse>
{
    public DeleteProductEndpoint() : base("/products/delete", "Delete Product", RequestType.Command)
    {
        this.serviceNames= new List<string> { "status400", "status404" };
    }

}
