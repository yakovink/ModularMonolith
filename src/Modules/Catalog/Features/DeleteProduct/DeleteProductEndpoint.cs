

namespace Catalog.Features.DeleteProduct;

public record DeleteProductResponse(bool isSuccess):GenericResponse<bool>(isSuccess);
public class DeleteProductEndpoint:GenericDeleteEndpoint<Guid,bool>
{
    public DeleteProductEndpoint() : base(
        "/products/delete",
         "Delete Product")
    {
        this.serviceNames= new List<string> {
             "status400",
              "status404"
              };
    }

    protected async override Task<IResult> NewEndpoint(GenericCommand<Guid,bool> request, ISender sender)
    {
        if (sender == null)
        {
            throw new InvalidOperationException("Sender is not set.");
        }
        //request
        DeleteProductCommand command = new DeleteProductCommand(request.input);
        //command
        //result
        DeleteProductResult result = await sender.Send(command);
        //response
        DeleteProductResponse response = new DeleteProductResponse(result.IsDeleted);
        //return the result
        return Results.Ok(response);
    }
}
