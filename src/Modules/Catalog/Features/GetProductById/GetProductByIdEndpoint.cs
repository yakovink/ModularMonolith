
namespace Catalog.Features.GetProductById;
public record GetProductByIdRequest(Guid Id):GenericQuery<Guid,ProductDto>(Id);
public record GetProductByIdResponse(ProductDto Product):GenericResponse<ProductDto>(Product);
internal class GetProductByIdEndpoint: GenericGetEndpoint<Guid, ProductDto>
{

    public GetProductByIdEndpoint() : base("/products/{id:guid}", "Get Product By Id")
    {
        this.serviceNames= new List<string> { "status400" };
    }

    protected async override Task<IResult> NewEndpoint(Guid request, ISender sender)
    {
        if (sender == null)
        {
            throw new InvalidOperationException("Sender is not set.");
        }
        //request
        GetProductByIdQuery command = new GetProductByIdQuery(request);
        //result
        GetProductByIdResult result = await sender.Send(command);
        //response
        GetProductByIdResponse response = new GetProductByIdResponse(result.Product);
        //return the result
        return Results.Ok(response);
    }
}