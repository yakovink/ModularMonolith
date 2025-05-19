using System;

namespace Shared.GenericRootModule.Features;

public abstract class GenericCommandEndpoint<T, V> : GenericEndpoint<T, V>
where T : notnull
{
    public GenericCommandEndpoint(string endpoint, string ActionName, List<string>? serviceNames = null) : base(endpoint, ActionName, serviceNames)
    {
    }

    protected abstract Task<IResult> NewEndpoint([AsParameters] GenericCommand<T, V> request, [FromServices] ISender sender);

}

public abstract class GenericPostEndpoint<T, V>(string endpoint, string ActionName, List<string>? serviceNames = null) : GenericCommandEndpoint<T, V>(endpoint, ActionName, serviceNames)
where T : notnull
{


    protected override RouteHandlerBuilder getMapMethod(IEndpointRouteBuilder app)
    {
        return app.MapPost(endpoint, (GenericCommand<T, V> request, ISender sender) => NewEndpoint(request, sender));
    }




}

public abstract class GenericDeleteEndpoint<T, V>(string endpoint, string ActionName, List<string>? serviceNames = null) : GenericCommandEndpoint<T, V>(endpoint, ActionName, serviceNames)
where T : notnull
{

    protected override RouteHandlerBuilder getMapMethod(IEndpointRouteBuilder app)
    {
        return app.MapDelete(endpoint, ([AsParameters] GenericCommand<T, V> request, [FromServices] ISender sender) => NewEndpoint(request, sender));
    }



}

public abstract class GenericPutEndpoint<T, V>(string endpoint, string ActionName, List<string>? serviceNames = null) : GenericCommandEndpoint<T, V>(endpoint, ActionName, serviceNames)
where T : notnull
{

    protected override RouteHandlerBuilder getMapMethod(IEndpointRouteBuilder app)
    {
        return app.MapPut(endpoint, ([AsParameters]GenericCommand<T, V> request, [FromServices]ISender sender) => NewEndpoint(request, sender));
    }

    

}