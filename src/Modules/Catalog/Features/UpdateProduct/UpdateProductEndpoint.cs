using System;
using Shared.GenericRootModule.Features;

namespace Catalog.Features.UpdateProduct;

public record UpdateProductRequest(ProductDto Product):GenericCommand<ProductDto,bool>(Product);

public record UpdateProductResponse(bool isSuccess):GenericResponse<bool>(isSuccess);

internal class UpdateProductEndpoint: GenericPostEndpoint<ProductDto, bool>
{
    public UpdateProductEndpoint() : base("/products/update", "Update Product")
    {
        this.serviceNames= new List<string> { "status400", "status404" };
    }

    protected async override Task<IResult> NewEndpoint(GenericCommand<ProductDto,bool> request, ISender sender)
    {
        if (sender == null)
        {
            throw new InvalidOperationException("Sender is not set.");
        }
        //request
        UpdateProductCommand command = new UpdateProductCommand(request.input);
        //command
        //result
        UpdateProductResult result = await sender.Send(command);
        //response
        UpdateProductResponse response = new UpdateProductResponse(result.isUpdated);
        //return the result
        return Results.Ok(response);
    }
}
