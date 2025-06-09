 

namespace Shared.GenericRootModule.Features;

public abstract class GenericQueryEndpoint<T, V> : GenericEndpoint<T, V>
where T : notnull
{
    public GenericQueryEndpoint(string endpoint, string ActionName, List<string>? serviceNames = null) : base(endpoint, ActionName, serviceNames)
    {
    }

    public async Task<IResult> SendResults(IQuery<GenericResult<V>> query, ISender sender)
    {
        GenericResult<V> result = await sender.Send(query);
        GenericResponse<V> response = result.Adapt<GenericResponse<V>>();
        
        return Results.Ok(response);
    }

    protected abstract Task<IResult> NewEndpoint(T input, ISender sender);

}

public abstract class GenericGetEndpoint<T, V>(string endpoint, string ActionName, List<string>? serviceNames = null) : GenericQueryEndpoint<T, V>(endpoint, ActionName, serviceNames)
where T : notnull
{

    protected override RouteHandlerBuilder getMapMethod(IEndpointRouteBuilder app)
    {
        if (IsSimpleType())
        {
            return app.MapGet(endpoint, ([FromQuery]T input, [FromServices] ISender sender) => {checkSender(sender); return NewEndpoint(input, sender);});
        }
        return app.MapGet(endpoint, ([AsParameters] T input, [FromServices] ISender sender) => {checkSender(sender); return NewEndpoint(input, sender);});
    }


}


