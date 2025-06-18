
namespace Catalog.Features.GetProducts;




internal class GetProductsEndpoint: CatalogModuleStructre.GetProducts.MGetEndpoint
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

        return await SendResults(new CatalogModuleStructre.GetProducts.Query (request), sender);

    }
}
