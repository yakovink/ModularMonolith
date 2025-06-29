

namespace Catalog.Features.GetProductsByCondition;



internal class GetProductByConditionEndpoint: CatalogModuleStructre.GetProductsByCondition.MGetEndpoint
{
    public GetProductByConditionEndpoint() : base(
        "/products/condition",
        "Get Product By Condition"
        )
    {
        this.serviceNames= new List<string> {
             "status400"
             };
    }

    protected async override Task<IResult> NewEndpoint(ProductDto request, ISender sender)
    {
        return await SendResults(new CatalogModuleStructre.GetProductsByCondition.Query(request),sender);
    }
}
