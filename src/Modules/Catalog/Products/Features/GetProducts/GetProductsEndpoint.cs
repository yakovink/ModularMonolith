
namespace Catalog.Features.GetProducts;




internal class GetProductsEndpoint:GenericGetEndpoint<PaginationRequest, PaginatedResult<ProductDto>>
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

        return await SendResults(new GetProductsQuery(request), sender);

    }
}
