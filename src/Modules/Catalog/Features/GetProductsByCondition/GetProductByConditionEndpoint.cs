
namespace Catalog.Features.GetProductsByCondition;


public record GetProductByConditionRequest(ProductDto Product);
public record GetProductByConditionResponse(HashSet<ProductDto> Products);

internal class GetProductByConditionEndpoint: GenericEndpoint<GetProductByConditionRequest,GetProductByConditionResponse>
{
    public GetProductByConditionEndpoint() : base("/products/condition/{condition}", "Get Product By Condition", RequestType.Query)
    {
        this.serviceNames= new List<string> { "status400" };
    }

}
