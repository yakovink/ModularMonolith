using System;

namespace Shared.GenericRootModule.Features;

public abstract class GenericCommandEndpoint<T, V> : GenericEndpoint<T, V>
where T : notnull
{
    public GenericCommandEndpoint(string endpoint, string ActionName, List<string>? serviceNames = null) : base(endpoint, ActionName, serviceNames)
    {
    }

    public async Task<IResult> SendResults(ICommand<GenericResult<V>> command, ISender sender)
    {
        GenericResult<V> result = await sender.Send(command);
        GenericResponse<V> response = new GenericResponse<V>(result.output);
        return Results.Ok(response);
    }

    protected abstract Task<IResult> NewEndpoint(GenericCommand<T, V> request, ISender sender);

}

public abstract class GenericPostEndpoint<T, V>(string endpoint, string ActionName, List<string>? serviceNames = null) : GenericCommandEndpoint<T, V>(endpoint, ActionName, serviceNames)
where T : notnull
{


    protected override RouteHandlerBuilder getMapMethod(IEndpointRouteBuilder app)
    {
        if (IsSimpleType())
        {
            return app.MapPost(endpoint, ([AsParameters] GenericCommand<T, V> request,[FromServices] ISender sender) => {checkSender(sender); return NewEndpoint(request, sender);});
        }
        return app.MapPost(endpoint, ([FromBody]GenericCommand<T, V> request,[FromServices] ISender sender) => {checkSender(sender); return NewEndpoint(request, sender);});
    }




}

public abstract class GenericDeleteEndpoint<T, V>(string endpoint, string ActionName, List<string>? serviceNames = null) : GenericCommandEndpoint<T, V>(endpoint, ActionName, serviceNames)
where T : notnull
{

    protected override RouteHandlerBuilder getMapMethod(IEndpointRouteBuilder app)
    {
        if (IsSimpleType()){
            return app.MapDelete(endpoint, ([AsParameters] GenericCommand<T, V> request, [FromServices] ISender sender) => {checkSender(sender); return NewEndpoint(request, sender);});
        }
        return app.MapDelete(endpoint, ( [FromBody]GenericCommand<T, V> request, [FromServices] ISender sender) => {checkSender(sender); return NewEndpoint(request, sender);});
    }



}

public abstract class GenericPutEndpoint<T, V>(string endpoint, string ActionName, List<string>? serviceNames = null) : GenericCommandEndpoint<T, V>(endpoint, ActionName, serviceNames)
where T : notnull
{

    protected override RouteHandlerBuilder getMapMethod(IEndpointRouteBuilder app)
    {
        if (IsSimpleType()){
            return app.MapPut(endpoint, ([AsParameters]GenericCommand<T, V> request, [FromServices] ISender sender) => {checkSender(sender); return NewEndpoint(request, sender);});
        }
        return app.MapPut(endpoint, ([FromBody]GenericCommand<T, V> request, [FromServices] ISender sender) => {checkSender(sender); return NewEndpoint(request, sender);});
    }

    

}