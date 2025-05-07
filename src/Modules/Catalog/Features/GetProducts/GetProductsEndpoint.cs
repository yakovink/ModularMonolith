namespace Catalog.Features.GetProducts;
public record GetProductsRequest();
public record GetProductsResponse(HashSet<ProductDto> Products);

internal class GetProductsEndpoint:GenericEndpoint<GetProductsRequest,GetProductsResponse>
{
    public GetProductsEndpoint() : base("/products", "Get Products", RequestType.Query)
    {
        this.serviceNames= new List<string> { "status400" };
    }


}
