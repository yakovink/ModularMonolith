
namespace Catalog.Features.GetProductsByCondition;


public record GetProductByConditionRequest(ProductDto Product):GenericRequest<ProductDto>(Product);
public record GetProductByConditionResponse(HashSet<ProductDto> Products):GenericResponse<HashSet<ProductDto>>(Products);

internal class GetProductByConditionEndpoint: GenericEndpoint<ProductDto, HashSet<ProductDto>>
{
    public GetProductByConditionEndpoint() : base("/products/condition/{condition}", "Get Product By Condition", RequestType.Query)
    {
        this.serviceNames= new List<string> { "status400" };
    }

}
