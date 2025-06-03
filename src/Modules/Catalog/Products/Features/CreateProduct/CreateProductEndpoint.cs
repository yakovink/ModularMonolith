


namespace Catalog.Features.CreateProduct;




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
        return await SendResults(new CreateProductCommand(request.input), sender);
    }


}
