

namespace Catalog.Features.GetProductById;


internal class GetProductByIdEndpoint: CatalogModuleStructre.GetProductById.MGetEndpoint
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
        return await SendResults(new CatalogModuleStructre.GetProductById.Query(input), sender);
    }
}