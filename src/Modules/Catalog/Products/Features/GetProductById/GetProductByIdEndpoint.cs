
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Features.GetProductById;


internal class GetProductByIdEndpoint: GenericGetEndpoint<Guid, ProductDto>
{

    public GetProductByIdEndpoint() : base(
        "/products/get",
        "Get Product By Id")
    {
        this.serviceNames= new List<string> {
            "status400"
            };
    }

    protected async override Task<IResult> NewEndpoint([FromQuery] Guid input, ISender sender)
    {
        return await SendResults(new GetProductByIdQuery(input), sender);
    }
}