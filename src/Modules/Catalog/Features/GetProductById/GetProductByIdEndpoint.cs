namespace Catalog.Features.GetProductById;
public record GetProductByIdRequest(Guid Id);
public record GetProductByIdResponse(ProductDto Product);
internal class GetProductByIdEndpoint: GenericEndpoint<GetProductByIdRequest,GetProductByIdResponse>
{

    public GetProductByIdEndpoint() : base("/products/{id:guid}", "Get Product By Id", RequestType.Query)
    {
        this.serviceNames= new List<string> { "status400" };
    }
}