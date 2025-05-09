using System;
using Catalog.Data;
using Microsoft.AspNetCore.Mvc;



namespace Shared.GenericRootModule.Features;




public record GenericRequest<T>(T input):ICommand<T>,IQuery<T>;

public record GenericResponse<V>(V Output);


public class GenericEndpoint<T,V>: ICarterModule



{

    protected string endpoint {set; get;}
    protected string ActionName {set; get;}
    protected RequestType type {set; get;}
    protected List<string>? serviceNames {set; get;}


    private static Dictionary<string,Func<RouteHandlerBuilder, RouteHandlerBuilder>> api =new Dictionary<string,Func<RouteHandlerBuilder, RouteHandlerBuilder>>{
        {"status400", builder=>builder.ProducesProblem(StatusCodes.Status400BadRequest)},
        {"status404", builder=>builder.ProducesProblem(StatusCodes.Status404NotFound)},
        {"status500", builder=>builder.ProducesProblem(StatusCodes.Status500InternalServerError)},
    };


    public GenericEndpoint<T,V> instance(){
        return this;

    }



    public GenericEndpoint (string endpoint,string ActionName, RequestType type,List<string>? serviceNames = null){

        this.endpoint=endpoint;
        this.ActionName=ActionName;
        this.type=type;
        this.serviceNames=serviceNames;
  
    }



    public void AddRoutes(IEndpointRouteBuilder app)
    {

        //Generic endpoint
        var get_lambda= newGetEndpoint;
        var post_lambda= newPostEndpoint;


        var builder = app.MapPost(endpoint,type.Equals(RequestType.Command)?post_lambda:get_lambda).WithName(ActionName)
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


private async Task<IResult> newGetEndpoint(T input, ISender sender)

{
    if (sender == null)
    {
        throw new InvalidOperationException("Sender is not set.");
    }
    //result
    var result=await sender.Send(new GenericRequest<T>(input));
    //response
    var response = result.Adapt<GenericResponse<V>>();
    //return the response
    return Results.Ok(response);
}





private async Task<IResult> newPostEndpoint(GenericRequest<T> request, ISender sender)
{
    if (sender == null)
    {
        throw new InvalidOperationException("Sender is not set.");
    }
    //command

    var command =request.Adapt<ICommand<T>>();
    //result

    
    var result = await sender.Send(command);
    
    //response
    var response = result.Adapt<GenericResponse<V>>();
    //return the result
    return Results.Ok(response);
}






private RouteHandlerBuilder Apply(RouteHandlerBuilder builder, string serviceName)
{
    if (!api.ContainsKey(serviceName))
    {
        throw new ArgumentException($"Service {serviceName} not found");
    }
    return api[serviceName](builder);
}

}
