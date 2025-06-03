
namespace Catalog.Features.GetProductById;


internal class GetProductByIdEndpoint: GenericGetEndpoint<Guid, ProductDto>
{

    public GetProductByIdEndpoint() : base(
        "/products/{id:guid}",
        "Get Product By Id")
    {
        this.serviceNames= new List<string> {
            "status400"
            };
    }

    protected async override Task<IResult> NewEndpoint(Guid request, ISender sender)
    {
        return await SendResults(new GetProductByIdQuery(request), sender);
    }
}