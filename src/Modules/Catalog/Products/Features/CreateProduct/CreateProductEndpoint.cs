


namespace Catalog.Features.CreateProduct;




internal class CreateProductEndpoint : CatalogModuleStructre.CreateProduct.MPostEndpoint
{

    public CreateProductEndpoint() : base(
        "/products/create",
        "Create Product")
    {
        this.serviceNames = new List<string> {
            "status400"
            };
    }

    protected async override Task<IResult> NewEndpoint(GenericCommandRequest<ProductDto,Guid> request, ISender sender)
    {
        return await SendResults(new CatalogModuleStructre.CreateProduct.Command(request.input), sender);
    }


}
