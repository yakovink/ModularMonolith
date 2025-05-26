

namespace Catalog.Features.GetProductsByCondition;



public record GetProductByConditionResponse(HashSet<ProductDto> Products):GenericResponse<HashSet<ProductDto>>(Products);

internal class GetProductByConditionEndpoint: GenericGetEndpoint<ProductDto, HashSet<ProductDto>>
{
    public GetProductByConditionEndpoint() : base(
        "/products/condition",
        "Get Product By Condition"
        )
    {
        this.serviceNames= new List<string> {
             "status400"
             };
    }

    protected async override Task<IResult> NewEndpoint(ProductDto request, ISender sender)
    {
        if (sender == null)
        {
            throw new InvalidOperationException("Sender is not set.");
        }

        //request
        GetProductsByConditionQuery command = new GetProductsByConditionQuery(request);
        //result
        GetProductsByConditionResult result = await sender.Send(command);
        //response
        GetProductByConditionResponse response = new GetProductByConditionResponse(result.Products);
        //return the result
        return Results.Ok(response);
    }
}
