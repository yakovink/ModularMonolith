
namespace Catalog.Features.GetProducts;
public record GetProductsRequest():GenericQuery<Object,HashSet<ProductDto>>('\0');
public record GetProductsResponse(HashSet<ProductDto> Products);

internal class GetProductsEndpoint:GenericGetEndpoint<object, HashSet<ProductDto>>
{
    public GetProductsEndpoint() : base("/products", "Get Products")
    {
        this.serviceNames= new List<string> { "status400" };
    }

    protected async override Task<IResult> NewEndpoint(Object obj, ISender sender)
    {
        if (sender == null)
        {
            throw new InvalidOperationException("Sender is not set.");
        }
        //request
        GetProductsQuery command = new GetProductsQuery();
        //result
        GetProductsResult result = await sender.Send(command);
        //response
        GetProductsResponse response = new GetProductsResponse(result.Products);
        //return the result
        return Results.Ok(response);
    }
}
