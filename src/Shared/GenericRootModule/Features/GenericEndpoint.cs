 




namespace Shared.GenericRootModule.Features;







public record GenericCommandRequest<T, V>(T input) : ICommand<V>;

public record GenericResult<V>(V output);
public record GenericResponse<V>{public required V output { get; init; }};


public abstract class GenericEndpoint<T, V>(string endpoint, string ActionName, List<string>? serviceNames = null) : ICarterModule

where T : notnull

{

    protected string endpoint { set; get; } = endpoint;
    protected string ActionName { set; get; } = ActionName;
    protected List<string>? serviceNames { set; get; } = serviceNames;


    private static Dictionary<string, Func<RouteHandlerBuilder, RouteHandlerBuilder>> api = new Dictionary<string, Func<RouteHandlerBuilder, RouteHandlerBuilder>>{
        {"status400", builder=>builder.ProducesProblem(StatusCodes.Status400BadRequest)},
        {"status404", builder=>builder.ProducesProblem(StatusCodes.Status404NotFound)},
        {"status500", builder=>builder.ProducesProblem(StatusCodes.Status500InternalServerError)},
    };
    



    public void AddRoutes(IEndpointRouteBuilder app)
    {

        //Generic endpoint



        var builder = getMapMethod(app).WithName(ActionName)
          .Produces<GenericResponse<V>>(StatusCodes.Status200OK)
          .WithDescription(ActionName)
          .WithSummary(ActionName);

        //apply the api
        if (serviceNames != null)
        {
            foreach (var serviceName in serviceNames)
            {
                builder = Apply(builder, serviceName);
            }
        }
    }



    protected abstract RouteHandlerBuilder getMapMethod(IEndpointRouteBuilder app);

    protected RouteHandlerBuilder Apply(RouteHandlerBuilder builder, string serviceName)
    {
        if (!api.ContainsKey(serviceName))
        {
            throw new ArgumentException($"Service {serviceName} not found");
        }
        return api[serviceName](builder);
    }

    public void checkSender(ISender sender)
    {
        if (sender == null)
            {
                throw new InvalidOperationException("Sender is not set.");
            }
    }
    
    protected bool IsSimpleType()
    {
        Type type = typeof(T);
        return
        type.IsPrimitive ||
        type.IsEnum ||
        type == typeof(string) ||
        type == typeof(decimal) ||
        type == typeof(Guid) ||
        type == typeof(DateTime) ||
        type == typeof(DateTimeOffset) ||
        type == typeof(TimeSpan);
    }

}










