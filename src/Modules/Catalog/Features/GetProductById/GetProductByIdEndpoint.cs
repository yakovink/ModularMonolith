namespace Catalog.Features.GetProductById;
public record GetProductByIdRequest(Guid Id):GenericRequest<Guid>(Id);
public record GetProductByIdResponse(ProductDto Product):GenericResponse<ProductDto>(Product);
internal class GetProductByIdEndpoint: GenericEndpoint<Guid, ProductDto>
{

    public GetProductByIdEndpoint() : base("/products/{id:guid}", "Get Product By Id", RequestType.Query)
    {
        this.serviceNames= new List<string> { "status400" };
    }
}