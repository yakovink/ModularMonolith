


namespace Catalog.Features.CreateProduct;


public record CreateProductResponse(Guid id):GenericResponse<Guid>(id);

internal class CreateProductEndpoint : GenericPostEndpoint<ProductDto, Guid>
{

    public CreateProductEndpoint() : base(
        "/products/create",
        "Create Product")
    {
        this.serviceNames = new List<string> {
            "status400"
            };
    }

    protected async override Task<IResult> NewEndpoint(GenericCommand<ProductDto,Guid> request, ISender sender)
    {
        if (sender == null)
        {
            throw new InvalidOperationException("Sender is not set.");
        }
        //request
        CreateProductCommand command = new CreateProductCommand(request.input);
        //result
        CreateProductResult result = await sender.Send(command);
        //response
        CreateProductResponse response = new CreateProductResponse(result.Id);
        //return the result
        return Results.Ok(response);
    }


}
