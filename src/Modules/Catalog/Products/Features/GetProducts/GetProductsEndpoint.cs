
namespace Catalog.Features.GetProducts;



public record GetProductsResponse(PaginatedResult<ProductDto> Products);

internal class GetProductsEndpoint:GenericGetEndpoint<PaginationRequest, HashSet<ProductDto>>
{
    public GetProductsEndpoint() : base(
        "/products",
        "Get Products"
        )
    {
        this.serviceNames= new List<string> {
             "status400"
             };
    }

    protected async override Task<IResult> NewEndpoint(PaginationRequest request, ISender sender)
    {
        if (sender == null)
        {
            throw new InvalidOperationException("Sender is not set.");
        }
        //request
        GetProductsQuery command = new GetProductsQuery(request);
        //result
        GetProductsResult result = await sender.Send(command);
        //response
        GetProductsResponse response = new GetProductsResponse(result.Products);
        //return the result
        return Results.Ok(response);
    }
}
