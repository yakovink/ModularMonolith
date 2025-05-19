using System;

namespace Shared.GenericRootModule.Features;

public abstract class GenericQueryEndpoint<T, V> : GenericEndpoint<T, V>
where T : notnull
{
    public GenericQueryEndpoint(string endpoint, string ActionName, List<string>? serviceNames = null) : base(endpoint, ActionName, serviceNames)
    {
    }

    protected abstract Task<IResult> NewEndpoint([AsParameters] T input, [FromServices] ISender sender);

}

public abstract class GenericGetEndpoint<T, V>(string endpoint, string ActionName, List<string>? serviceNames = null) : GenericQueryEndpoint<T, V>(endpoint, ActionName, serviceNames)
where T : notnull
{

    protected override RouteHandlerBuilder getMapMethod(IEndpointRouteBuilder app)
    {
        Console.WriteLine(GetType());
        Console.WriteLine(typeof(T));
        if (IsSimpleType())
        {
            Console.WriteLine("is simple");
            return app.MapGet(endpoint, (T input, [FromServices] ISender sender) => NewEndpoint(input, sender));
        }
        Console.WriteLine("is not simple");
        return app.MapGet(endpoint, ([AsParameters] T input, [FromServices] ISender sender) => NewEndpoint(input, sender));
    }


}