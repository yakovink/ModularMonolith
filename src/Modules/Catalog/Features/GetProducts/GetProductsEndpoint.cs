namespace Catalog.Features.GetProducts;
public record GetProductsRequest(Object? obj=null):GenericRequest<Object?>(obj);
public record GetProductsResponse(HashSet<ProductDto> Products);

internal class GetProductsEndpoint:GenericEndpoint<Object?, HashSet<ProductDto>>
{
    public GetProductsEndpoint() : base("/products", "Get Products", RequestType.Query)
    {
        this.serviceNames= new List<string> { "status400" };
    }


}
